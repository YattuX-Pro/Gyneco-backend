using Gyneco.Domain;
using Gyneco.Domain.Contracts.Persistence;
using Gyneco.Persistence.DatabaseContext;

namespace Kada.persistence.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(GynecoDbContext context) : base(context)
    {
    }
}