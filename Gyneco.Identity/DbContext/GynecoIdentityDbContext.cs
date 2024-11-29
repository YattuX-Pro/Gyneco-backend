using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain;
using Gyneco.Domain.Identity;

namespace Gyneco.Identity.DbContext
{
    public class GynecoIdentityDbContext : IdentityDbContext<ApplicationUser, ApplicationUserRoles, Guid>
    {
        public GynecoIdentityDbContext(DbContextOptions<GynecoIdentityDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(GynecoIdentityDbContext).Assembly);

            builder.Entity<ApplicationUser>()
           .HasMany(e => e.UserRoles)
           .WithOne(w => w.User)
           .HasForeignKey(ur => ur.UserId)
           .IsRequired();

            builder.Entity<ApplicationUserRoles>()
           .HasMany(e => e.UserRoles)
           .WithOne(w => w.Role)
           .HasForeignKey(ur => ur.RoleId)
           .IsRequired();
            
            builder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
