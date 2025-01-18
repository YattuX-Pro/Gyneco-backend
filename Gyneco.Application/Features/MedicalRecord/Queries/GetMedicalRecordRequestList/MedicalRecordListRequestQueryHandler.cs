using Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestDetail;
using Gyneco.Application.Models.Search;
using Gyneco.Domain.Contracts.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyneco.Application.Features.MedicalRecord.Queries.GetMedicalRecordRequestList;

public class MedicalRecordListRequestQueryHandler : IRequestHandler<MedicalRecordListRequestQuery,SearchResult<MedicalRecordDetailDTO>>
{
    private IUnitOfWork _unitOfWork;

    public MedicalRecordListRequestQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<SearchResult<MedicalRecordDetailDTO>> Handle(MedicalRecordListRequestQuery request, CancellationToken cancellationToken)
    {
        var filteredQuery = GetMedicalRecordQuery(request.Filters);
        var filteredResult = (request.PageIndex == -1)
            ? await filteredQuery.ToListAsync()
            : await filteredQuery.Skip(request.PageIndex * request.PageSize).Take(request.PageSize).ToListAsync();
        var row = new List<MedicalRecordDetailDTO>();

        foreach (var medicalRecord in filteredResult)
        {
            row.Add(new MedicalRecordDetailDTO()
            {
                Id = medicalRecord.Id,
                PatientId = medicalRecord.PatientId,
                Description = medicalRecord.Description,
                CreatedBy = medicalRecord.CreatedBy,
                Patient = medicalRecord.Patient,
                DateCreated = medicalRecord.DateCreated,
                DateModified = medicalRecord.DateModified,
                ModifiedBy = medicalRecord.ModifiedBy,
                RecordType = medicalRecord.RecordType,
                DateOfRecord = medicalRecord.DateOfRecord
            });
        }

        return new SearchResult<MedicalRecordDetailDTO>()
        {
            Results = row,
            TotalCount = row.Count,
            CountPerPage = request.PageSize,
            Page = request.PageIndex,
        };
    }

    private IQueryable<Domain.MedicalRecord> GetMedicalRecordQuery(Dictionary<string, string> filters)
    {
        var medicalRecordQuery = _unitOfWork.MedicalRecordRepository.GetQuery("Patient");

        foreach (var key in filters.Keys)
        {
            if(string.IsNullOrEmpty(key)) continue;
            switch (key)
            {
                case "patientId":
                    medicalRecordQuery = _unitOfWork.MedicalRecordRepository.FilterQuery(medicalRecordQuery, x=> x.PatientId == Guid.Parse(filters[key]));
                    break;
                case "patientName":
                    medicalRecordQuery = _unitOfWork.MedicalRecordRepository.FilterQuery(medicalRecordQuery, 
                        x=> (x.Patient.FirstName + " " + x.Patient.LastName).Contains(filters[key]));
                    break;
            }
        }
        
        return medicalRecordQuery;
    }
}