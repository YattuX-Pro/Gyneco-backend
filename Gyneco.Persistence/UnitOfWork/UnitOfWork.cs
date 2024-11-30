using Gyneco.Domain.Contracts.Persistence;
using Gyneco.Domain.Contracts.UnitOfWork;
using Gyneco.Persistence.DatabaseContext;
using Kada.persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IPatientRepository PatientRepository { get; private set; }
    public IDoctorRepository DoctorRepository { get; private set; }
    public IClinicRepository ClinicRepository { get; private set; }
    public IMedicalRecordRepository MedicalRecordRepository { get; private set; }
    public IPaymentRepository PaymentRepository { get; private set; }
    public IPrescriptionRepository PrescriptionRepository { get; private set; }
    public IScheduleRepository ScheduleRepository { get; private set; }
    public IAppointmentRepository AppointmentRepository { get; private set; }

    public UnitOfWork(GynecoDbContext gynecoDbContext)
    {
        PatientRepository = new PatientRepository(gynecoDbContext);
        DoctorRepository = new DoctorRepository(gynecoDbContext);
        ClinicRepository = new ClinicRepository(gynecoDbContext);
        MedicalRecordRepository = new MedicalRecordRepository(gynecoDbContext);
        PaymentRepository = new PaymentRepository(gynecoDbContext);
        PrescriptionRepository = new PrescriptionRepository(gynecoDbContext);
        ScheduleRepository = new ScheduleRepository(gynecoDbContext);
        AppointmentRepository = new AppointmentRepository(gynecoDbContext);
    }
}