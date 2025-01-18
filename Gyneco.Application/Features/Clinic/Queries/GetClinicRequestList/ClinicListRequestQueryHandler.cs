using Gyneco.Application.Features.Clinic.Queries.GerClinicRequestDetail;
using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyneco.Application.Features.Clinic.Queries.GetClinicRequestList;

public class ClinicListRequestQueryHandler : IRequestHandler<ClinicListRequestQuery, SearchResult<ClinicDetailDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ClinicListRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<SearchResult<ClinicDetailDTO>> Handle(ClinicListRequestQuery request, CancellationToken cancellationToken)
    {
        var filteredQuery = GetClinicQuery(request.Filters);
        var filteredResult =  (request.PageIndex == -1)
            ? await filteredQuery.ToListAsync()
            : await filteredQuery.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();
        var row = new List<ClinicDetailDTO>();

        foreach (var clinic in filteredResult)
        {
            row.Add(new ClinicDetailDTO()
            {
                Id = clinic.Id,
                Name = clinic.Name,
                Email = clinic.Email,
                PhoneNumber = clinic.PhoneNumber,
                Address = clinic.Address,
                CreatedBy = clinic.CreatedBy,
                DateCreated = clinic.DateCreated,
                DateModified = clinic.DateModified,
                ModifiedBy = clinic.ModifiedBy,
            });
        }

        return new SearchResult<ClinicDetailDTO>()
        {
            TotalCount = filteredQuery.Count(),
            Results = row,
            CountPerPage = request.PageSize,
            Page = request.PageIndex,
        };
    }

    private IQueryable<Domain.Clinic> GetClinicQuery(Dictionary<string, string> filters)
    {
        var clinicQuery = _unitOfWork.ClinicRepository.GetQuery();

        foreach (var key in filters.Keys)
        {
            if(string.IsNullOrEmpty(key)) continue;

            switch (key)
            {
                case "Id":
                    clinicQuery = _unitOfWork.ClinicRepository.FilterQuery(clinicQuery, x => x.Id == Guid.Parse(filters[key]));
                    break;
                case "Name":
                    clinicQuery = _unitOfWork.ClinicRepository.FilterQuery(clinicQuery, x => x.Name == filters[key]);
                    break;
                case "Address":
                    clinicQuery = _unitOfWork.ClinicRepository.FilterQuery(clinicQuery, x => x.Address == filters[key]);
                    break;
                case "PhoneNumber":
                    clinicQuery = _unitOfWork.ClinicRepository.FilterQuery(clinicQuery, x => x.PhoneNumber == filters[key]);
                    break;
                case "Email":
                    clinicQuery = _unitOfWork.ClinicRepository.FilterQuery(clinicQuery, x => x.Email == filters[key]);
                    break;
            }
        }
        return clinicQuery;
    }
}