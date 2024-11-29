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
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.HasData(
                new UserRoles
                {
                    RoleId = new Guid("0e2d2d97-14d4-49fa-8616-d9711e4bab9d"),
                    UserId = new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b")
                },
                new UserRoles
                {
                    RoleId = new Guid("6a41b122-c556-4f2e-8b87-948f115f274c"),
                    UserId = new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e")
                },
                new UserRoles
                {
                    RoleId = new Guid("57838d8b-8b16-4b01-8018-a6b316785d8e"),
                    UserId = new Guid("a9fce60c-1947-4313-b124-9300d1b4a127")
                }
            );
        }
    }
}
