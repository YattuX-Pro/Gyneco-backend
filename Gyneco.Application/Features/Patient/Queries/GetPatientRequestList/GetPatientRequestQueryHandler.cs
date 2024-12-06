using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gyneco.Application.Features.Patient.Queries.GetPatientRequestList;

public class GetPatientRequestQueryHandler : IRequestHandler<GetPatientRequestQuery, SearchResult<GetPatientRequestDTO>>
{
    private readonly IUnitOfWork _uow;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetPatientRequestQueryHandler(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
    {
        _uow = uow;
        _userManager = userManager;
    }
    
    public Task<SearchResult<GetPatientRequestDTO>> Handle(GetPatientRequestQuery request, CancellationToken cancellationToken)
    {
        var filteredRequest = GetPatientQuery(request.Filters);
        var filteredPatient = (request.PageIndex == -1) ? filteredRequest.ToList() : filteredRequest.Skip(request.PageIndex  * request.PageSize).Take(request.PageSize).ToList();
        var row = new List<GetPatientRequestDTO>();

        foreach (var patient in filteredPatient)
        {
            row.Add(new GetPatientRequestDTO
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
                User = _userManager.Users.SingleOrDefault(u => u.Id == patient.UserId)
            });
        }
        
        var result = new SearchResult<GetPatientRequestDTO>
        {
            Results = row,
            TotalCount = row.Count,
            CountPerPage = request.PageSize,
            Page = request.PageIndex,
        };
        return Task.FromResult(result);
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