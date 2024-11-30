using Gyneco.Domain;
using Gyneco.Domain.Contracts.Persistence;
using Gyneco.Persistence.DatabaseContext;
using Kada.persistence.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(GynecoDbContext context) : base(context)
    {
        
    }
}