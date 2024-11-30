using Gyneco.Domain.Contracts.Persistence;

namespace Gyneco.Domain.Contracts.UnitOfWork;

public interface IUnitOfWork
{
    public IPatientRepository PatientRepository { get; }
    public IDoctorRepository DoctorRepository { get; }
    public IClinicRepository ClinicRepository { get; }
    public IMedicalRecordRepository MedicalRecordRepository { get; }
    public IPaymentRepository PaymentRepository { get; }
    public IPrescriptionRepository PrescriptionRepository { get; }
    public IScheduleRepository ScheduleRepository { get; }
    public IAppointmentRepository AppointmentRepository { get; }
}