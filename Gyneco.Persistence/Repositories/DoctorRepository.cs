using Gyneco.Domain;
using Gyneco.Domain.Contracts.Persistence;
using Gyneco.Persistence.DatabaseContext;

namespace Kada.persistence.Repositories;

public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(GynecoDbContext context) : base(context)
    {
    }
}