using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gyneco.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIndentityUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0e2d2d97-14d4-49fa-8616-d9711e4bab9d"), new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("57838d8b-8b16-4b01-8018-a6b316785d8e"), new Guid("a9fce60c-1947-4313-b124-9300d1b4a127") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6a41b122-c556-4f2e-8b87-948f115f274c"), new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e") });

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[,]
                {
                    { new Guid("0e2d2d97-14d4-49fa-8616-d9711e4bab9d"), new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b"), "UserRoles" },
                    { new Guid("57838d8b-8b16-4b01-8018-a6b316785d8e"), new Guid("a9fce60c-1947-4313-b124-9300d1b4a127"), "UserRoles" },
                    { new Guid("6a41b122-c556-4f2e-8b87-948f115f274c"), new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e"), "UserRoles" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3937f108-9160-434c-8ce6-3581b4e24857", "AQAAAAIAAYagAAAAEKQ9WHfNb5Pw/nP4Kuvvx3RaW88+Tqs811DsbBJy8ti/L8UXhtQrb4do2Lze7C5Bdg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a9fce60c-1947-4313-b124-9300d1b4a127"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "22efa1de-f629-4101-a482-bd1557cb4c13", "AQAAAAIAAYagAAAAEA/HFFdL1S++FGsU6QcBpiolAQODVjvDrK6Ohhkvdzws8c/6K03vT3uq53WqgffJRA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1ebefff6-9668-4914-857d-ffa91b23f50f", "AQAAAAIAAYagAAAAEESarr4/mjda9+A5hCon+mefCfXj5vt7srKTeI2pX+7R2MBPJIU6Br0bqcg/IQji+Q==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0ddf4dee-a3aa-4be8-9318-4f1e75913609", "AQAAAAIAAYagAAAAEO2Fs1vwkIS5vJRKcqY1zvSF0iML6pe7XJuXx+BL7Byi9JBgs88HXy6cOPmXjlZTyw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a9fce60c-1947-4313-b124-9300d1b4a127"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "766f050b-7704-45f9-a31b-fefc808772e2", "AQAAAAIAAYagAAAAEF8N2PGpDmDZF/zMEegbJyVK0hFxnepOeRJPq9Zk8Revf0ub076ZXD47PybqLmz6xA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f3440754-0140-4e5d-abd5-c65bb36b4688", "AQAAAAIAAYagAAAAEOhoiha33094fFqzp947OcVg3jO6Migd/BOPW0W1Cb3y9SdTiiWGre1SPWAJNiCmuQ==" });
        }
    }
}
