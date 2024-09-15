using Gyneco.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyneco.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<ApplicationUserRoles>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoles> builder)
        {
            builder.HasData(
                new ApplicationUserRoles
                {
                    Id = new Guid("0e2d2d97-14d4-49fa-8616-d9711e4bab9d"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new ApplicationUserRoles
                {
                    Id = new Guid("6a41b122-c556-4f2e-8b87-948f115f274c"),
                    Name = "Doctor",
                    NormalizedName = "DOCTOR"
                },
                new ApplicationUserRoles
                {
                    Id = new Guid("57838d8b-8b16-4b01-8018-a6b316785d8e"),
                    Name = "Patient",
                    NormalizedName = "PATIENT"
                }
            );
        }
    }
}
