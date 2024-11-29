using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gyneco.Domain.Identity;

namespace Gyneco.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b"),
                     Email = "admin@localhost.com",
                     NormalizedEmail = "ADMIN@LOCALHOST.COM",
                     FirstName = "System",
                     LastName = "Admin",
                     UserName = "admin@localhost.com",
                     NormalizedUserName = "ADMIN@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true,
                 },
                 new ApplicationUser
                 {
                     Id = new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e"),
                     Email = "doctor@localhost.com",
                     NormalizedEmail = "DOCTOR@LOCALHOST.COM",
                     FirstName = "System",
                     LastName = "doctor",
                     UserName = "doctor@localhost.com",
                     NormalizedUserName = "DOCTOR@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true,
                 },
                 new ApplicationUser
                 {
                     Id = new Guid("a9fce60c-1947-4313-b124-9300d1b4a127"),
                     Email = "patient@localhost.com",
                     NormalizedEmail = "PATIENT@LOCALHOST.COM",
                     FirstName = "System",
                     LastName = "patient",
                     UserName = "patient@localhost.com",
                     NormalizedUserName = "PATIENT@LOCALHOST.COM",
                     PasswordHash = hasher.HashPassword(null, "P@ssword1"),
                     EmailConfirmed = true,
                 }
            );
        }
    }
}
