using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gyneco.Identity.Migrations
{
    /// <inheritdoc />
    public partial class users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("32173892-2e8f-4f7b-a059-8d88eb21482b"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7568d068-6aab-48ab-95cb-d0ba5394a669", "AQAAAAIAAYagAAAAEIzIL0yV6uNm6mp7VhQtNtWImUo2f4Y0awAhq5Ei2u8GrIUqgQYdM5SSMT5TB4oN8g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a9fce60c-1947-4313-b124-9300d1b4a127"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0c8a4234-7c5b-456e-bd8a-93e3ec1a9c46", "AQAAAAIAAYagAAAAEMl5mXl3hojGWmiPLNjiDQ1Z29275z9safrKzPmKRNkyB1YFnTEyB1H0QeuQ0mW+Eg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e19eb100-f7b6-4081-b6ab-412958bb700e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42610232-a7fb-4de0-8ef8-3c5cd6aab4d2", "AQAAAAIAAYagAAAAEOhfaNKp3I5gL+i37K5yR2Qig60uWHkgJMcnKjbcyREHRpI8IaUxMwK/qPZY7gQkug==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
