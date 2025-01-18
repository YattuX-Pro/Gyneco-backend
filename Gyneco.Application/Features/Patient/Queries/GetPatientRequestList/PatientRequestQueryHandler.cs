using Gyneco.Application.Features.Patient.Queries.GetPatientRequestDetail;
using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;

public class PatientRequestQueryHandler : IRequestHandler<PatientRequestQuery, SearchResult<PatientDetailDTO>>
{
    private readonly IUnitOfWork _uow;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatientRequestQueryHandler(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
    {
        _uow = uow;
        _userManager = userManager;
    }
    
    public async Task<SearchResult<PatientDetailDTO>> Handle(PatientRequestQuery request, CancellationToken cancellationToken)
    {
        var filteredRequest = GetPatientQuery(request.Filters);
        var filteredPatient = (request.PageIndex == -1) 
            ? filteredRequest.ToList() 
            : filteredRequest.Skip(request.PageIndex  * request.PageSize).Take(request.PageSize).ToList();
        var row = new List<PatientDetailDTO>();

        foreach (var patient in filteredPatient)
        {
            row.Add(new PatientDetailDTO
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                Address = patient.Address,
                UserId = patient.UserId,
                User = await _uow.UserService.GetUserAsync(patient.UserId)
            });
        }
        
        var result = new SearchResult<PatientDetailDTO>
        {
            Results = row,
            TotalCount = row.Count,
            CountPerPage = request.PageSize,
            Page = request.PageIndex,
        };
        return result;
    }

    private IQueryable<Domain.Patient> GetPatientQuery(Dictionary<string, string> filters)
    {
        IQueryable<Domain.Patient> patientQuery = _uow.PatientRepository.GetQuery();

        foreach (var key in filters.Keys)
        {
            if(string.IsNullOrEmpty(key)) continue;
            switch (key)
            {
                case "lastName" : 
                    patientQuery = _uow.PatientRepository.FilterQuery(patientQuery, x => x.LastName == filters[key]);
                    break;
                case "firstName" :
                    patientQuery  = _uow.PatientRepository.FilterQuery(patientQuery, x => x.FirstName == filters[key]);
                    break;
                case "email" :
                    patientQuery = _uow.PatientRepository.FilterQuery(patientQuery, x => x.Email == filters[key]);
                    break;
                case "phoneNumber" :
                    patientQuery = _uow.PatientRepository.FilterQuery(patientQuery, x => x.PhoneNumber == filters[key]);
                    break;
            }
        }
        return patientQuery;
    }
}