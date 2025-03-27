using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDate", "CityId", "FirstName", "Gender", "LastName", "PersonalNumber", "ProfilePicture" },
                values: new object[,]
                {
                    { -2, new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Jane", "Male", "Smith", "98765432101", null },
                    { -1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "John", "Male", "Doe", "12345678901", null }
                });

            migrationBuilder.InsertData(
                table: "ConnectedPersons",
                columns: new[] { "Id", "ConnectedPersonId", "ConnectionType", "PersonId" },
                values: new object[] { -1, -2, "Colleague", -1 });

            migrationBuilder.InsertData(
                table: "PhoneNumbers",
                columns: new[] { "Id", "Number", "PersonId", "PhoneType" },
                values: new object[,]
                {
                    { -2, "+987654321", -2, "Home" },
                    { -1, "+123456789", -1, "Mobile" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConnectedPersons",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "PhoneNumbers",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: -1);
        }
    }
}
