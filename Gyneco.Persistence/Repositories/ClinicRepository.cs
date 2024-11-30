using Gyneco.Domain;
using Gyneco.Domain.Contracts.Persistence;
using Gyneco.Persistence.DatabaseContext;

namespace Kada.persistence.Repositories;

public class ClinicRepository : GenericRepository<Clinic>, IClinicRepository
{
    public ClinicRepository(GynecoDbContext context) : base(context)
    {
    }
}