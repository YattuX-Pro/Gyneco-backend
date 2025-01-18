using Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestDetail;
using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;

namespace Gyneco.Application.Features.Appointment.Queries.GetAppointmentRequestList;

public class AppointmentListRequestQueryHandler : IRequestHandler<AppointmentListRequestQuery, SearchResult<AppointmentDetailDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public AppointmentListRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<SearchResult<AppointmentDetailDTO>> Handle(AppointmentListRequestQuery listRequest, CancellationToken cancellationToken)
    {
        var filteredQuery = GetAppointmentQuery(listRequest.Filters);
        var filteredResults = listRequest.PageIndex == -1 
            ? filteredQuery.ToList() 
            : filteredQuery.Skip(listRequest.PageIndex * listRequest.PageSize).Take(listRequest.PageSize).ToList();
        var row = new List<AppointmentDetailDTO>();
        foreach (var appointment in filteredResults)
        {
            row.Add(new AppointmentDetailDTO()
            {
                Doctor = appointment.Doctor,
                AppointmentDate = appointment.AppointmentDate,
                Patient = appointment.Patient,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                Status = appointment.Status,
                ReasonForVisit = appointment.ReasonForVisit,
            });
        }

        return new SearchResult<AppointmentDetailDTO>()
        {
            Results = row,
            TotalCount = filteredQuery.Count(),
            CountPerPage = filteredQuery.Count(),
            Page = listRequest.PageIndex
        };
    }

    private IQueryable<Domain.Appointment> GetAppointmentQuery(Dictionary<string, string> filters)
    {
        var appointmentQuery = _unitOfWork.AppointmentRepository.GetQuery("Doctor,Patient");

        foreach (var key in filters.Keys)
        {
            if(string.IsNullOrEmpty(filters[key])) continue;
            switch (key)
            {
                case "DoctorId":
                    appointmentQuery = _unitOfWork.AppointmentRepository.FilterQuery(appointmentQuery, x => x.DoctorId == new Guid(filters[key]));
                    break;
                case "DoctorName":
                    appointmentQuery = _unitOfWork.AppointmentRepository.FilterQuery(appointmentQuery,
                        x => (x.Doctor.FirstName + " " + x.Doctor.LastName).ToLower().Contains(filters[key].ToLower()));
                    break;
                case "PatientId":
                    appointmentQuery = _unitOfWork.AppointmentRepository.FilterQuery(appointmentQuery, x => x.PatientId == new Guid(filters[key]));
                    break;
                case "PatientName":
                    appointmentQuery = _unitOfWork.AppointmentRepository.FilterQuery(appointmentQuery, 
                        x => (x.Patient.FirstName + " " + x.Patient.LastName).ToLower().Contains(filters[key].ToLower()));
                    break;
                case "appointementDateTime":
                    appointmentQuery = _unitOfWork.AppointmentRepository.FilterQuery(appointmentQuery, x=> x.AppointmentDate.ToShortDateString() == filters[key]);
                    break;
            }
        }
        return appointmentQuery;
    }
}