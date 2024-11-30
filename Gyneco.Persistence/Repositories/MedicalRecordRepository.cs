using Gyneco.Domain;
using Gyneco.Domain.Contracts.Persistence;
using Gyneco.Persistence.DatabaseContext;

namespace Kada.persistence.Repositories;

public class MedicalRecordRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(GynecoDbContext context) : base(context)
    {
    }
}