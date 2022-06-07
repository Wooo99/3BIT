using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.DAL.Migrations
{
    public partial class SetOnDeleteActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Cars_CarId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_DriverGuid",
                table: "Rides");

            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("9aedbefa-b882-4413-a9ae-b3446c22d960"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("a428c96b-0061-4be5-86fa-964ae3fc3f87"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("e9f07bb1-ff1b-40e9-bf8a-47e217d5c484"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("d42cebb4-75e1-4119-bf83-47c36949b66d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("425305cd-758b-4537-b26a-8db497ca5b12"));

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("a61c020d-401e-43da-8053-6eaa27dda904"), "Olomouc", "Czech", null, "Neredin 2", "779 00" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("a7f74cd2-0bcd-4b66-8d26-1e7ad7333bed"), "Brno", "Czech", null, "Kolejni 5", "61600" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PhotoPath" },
                values: new object[] { new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"), "Adam", "Kovacik", null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "Manufacturer", "Name", "OwnerGuid", "PassengerSeats", "PhotoPath", "RegistrationYear" },
                values: new object[] { new Guid("30f2b280-90f8-4902-b5da-40619242a208"), 2, 48, "test name", new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"), 4, null, 2000 });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "CarId", "DestinationGuid", "DriverGuid", "EstimatedEndTime", "StartGuid", "StartTime" },
                values: new object[] { new Guid("b3921442-5f51-49db-9518-b91436733b33"), new Guid("30f2b280-90f8-4902-b5da-40619242a208"), new Guid("a61c020d-401e-43da-8053-6eaa27dda904"), new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"), new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a7f74cd2-0bcd-4b66-8d26-1e7ad7333bed"), new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Cars_CarId",
                table: "Rides",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_DriverGuid",
                table: "Rides",
                column: "DriverGuid",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Cars_CarId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Users_DriverGuid",
                table: "Rides");

            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("b3921442-5f51-49db-9518-b91436733b33"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("a61c020d-401e-43da-8053-6eaa27dda904"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("a7f74cd2-0bcd-4b66-8d26-1e7ad7333bed"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("30f2b280-90f8-4902-b5da-40619242a208"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"));

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("a428c96b-0061-4be5-86fa-964ae3fc3f87"), "Olomouc", "Czech", null, "Neredin 2", "779 00" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("e9f07bb1-ff1b-40e9-bf8a-47e217d5c484"), "Brno", "Czech", null, "Kolejni 5", "61600" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PhotoPath" },
                values: new object[] { new Guid("425305cd-758b-4537-b26a-8db497ca5b12"), "Adam", "Kovacik", null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "Manufacturer", "Name", "OwnerGuid", "PassengerSeats", "PhotoPath", "RegistrationYear" },
                values: new object[] { new Guid("d42cebb4-75e1-4119-bf83-47c36949b66d"), 2, 48, "test name", new Guid("425305cd-758b-4537-b26a-8db497ca5b12"), 4, null, 2000 });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "CarId", "DestinationGuid", "DriverGuid", "EstimatedEndTime", "StartGuid", "StartTime" },
                values: new object[] { new Guid("9aedbefa-b882-4413-a9ae-b3446c22d960"), new Guid("d42cebb4-75e1-4119-bf83-47c36949b66d"), new Guid("a428c96b-0061-4be5-86fa-964ae3fc3f87"), new Guid("425305cd-758b-4537-b26a-8db497ca5b12"), new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e9f07bb1-ff1b-40e9-bf8a-47e217d5c484"), new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Cars_CarId",
                table: "Rides",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Users_DriverGuid",
                table: "Rides",
                column: "DriverGuid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
