﻿using Gyneco.Domain;
using Gyneco.Domain.Common;
using Gyneco.Domain.Contracts.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gyneco.Persistence.DatabaseContext
{
    public class GynecoDbContext : DbContext
    {
        private readonly IUserService _userService;
        public GynecoDbContext(
                                DbContextOptions<GynecoDbContext> options, 
                                IUserService userService) : base(options) 
        {
            _userService = userService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GynecoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<Patient>();
            modelBuilder.Ignore<Doctor>();
            modelBuilder.Ignore<Schedule>();
            modelBuilder.Ignore<Appointment>();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                entry.Entity.ModifiedBy = _userService.UserId;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = _userService.UserId;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Prescription> Prescription { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
    }
}
