using Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestDetail;
using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyneco.Application.Features.Doctor.Queries.GetDoctorRequestList;

public class DoctorListRequestQueryHandler : IRequestHandler<DoctorListRequestQuery, SearchResult<DoctorDetailDTO>>
{
    private readonly IUnitOfWork _uow;

    public DoctorListRequestQueryHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    public async Task<SearchResult<DoctorDetailDTO>> Handle(DoctorListRequestQuery request, CancellationToken cancellationToken)
    {
        var filteredRequest = GetDoctorQuery(request.Filters);
        var doctorRequest = request.PageIndex == -1
            ? await filteredRequest.ToListAsync()
            : await filteredRequest.Skip(request.PageIndex).Take(request.PageSize * request.PageSize).ToListAsync();
        var row = new List<DoctorDetailDTO>();

        foreach (var doctor in doctorRequest)
        {
            row.Add( new DoctorDetailDTO
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Email = doctor.Email,
                    PhoneNumber = doctor.PhoneNumber,
                    Specialty = doctor.Specialty,
                    UserId = doctor.UserId,
                    Appointments = doctor.Appointments,
                    Schedules = doctor.Schedules,
                    CreatedBy = doctor.CreatedBy,
                    DateCreated = doctor.DateCreated,
                    ModifiedBy = doctor.ModifiedBy,
                    DateModified = doctor.DateModified, 
                    User = await _uow.UserService.GetUserAsync(doctor.UserId)
                });
        }

        return new SearchResult<DoctorDetailDTO>()
        {
            Results = row,
            TotalCount = row.Count,
            CountPerPage = request.PageSize,
            Page = request.PageIndex,
        };
    }

    private IQueryable<Domain.Doctor> GetDoctorQuery(Dictionary<string, string> filters)
    {
        var doctorQuery = _uow.DoctorRepository.GetQuery("Appointments,Schedules");

        foreach (var key in filters.Keys)
        {
            if(filters[key] is null) continue;

            switch (key)
            {
                case "FirstName":
                    doctorQuery = _uow.DoctorRepository.FilterQuery(doctorQuery, p => p.FirstName == filters[key]);
                    break;
                case "LastName":
                    doctorQuery = _uow.DoctorRepository.FilterQuery(doctorQuery, p => p.LastName == filters[key]);
                    break;
                case "PhoneNumber":
                    doctorQuery = _uow.DoctorRepository.FilterQuery(doctorQuery, p => p.PhoneNumber == filters[key]);
                    break;
                case "Specialty":
                    doctorQuery = _uow.DoctorRepository.FilterQuery(doctorQuery, p => p.Specialty == Enum.Parse<DoctorSpeciality>(filters[key]));
                    break;
            }
        }

        return doctorQuery;
    }
}