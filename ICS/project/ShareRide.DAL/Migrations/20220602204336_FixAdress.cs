using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.DAL.Migrations
{
    public partial class FixAdress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Rides_RideId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Addresses_DestinationGuid",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_RideId",
                table: "Addresses");

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

            migrationBuilder.DropColumn(
                name: "RideId",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressEntityId",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Street", "ZIP" },
                values: new object[] { new Guid("a48d8274-371d-4acc-b51f-3d973176359e"), "Brno", "Czech", "Kolejni 5", "61600" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "Street", "ZIP" },
                values: new object[] { new Guid("c496ff01-5c7c-4296-9f3c-598b6e0d698c"), "Olomouc", "Czech", "Neredin 2", "779 00" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PhotoPath" },
                values: new object[] { new Guid("bffa295b-ff0c-4c60-877b-2ca3b84d0a0e"), "Adam", "Kovacik", null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "Manufacturer", "Name", "OwnerGuid", "PassengerSeats", "PhotoPath", "RegistrationYear" },
                values: new object[] { new Guid("30e1461d-61e5-40e2-bb3e-7a4b681c7b7b"), 2, 48, "test name", new Guid("bffa295b-ff0c-4c60-877b-2ca3b84d0a0e"), 4, null, 2000 });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "AddressEntityId", "CarId", "DestinationGuid", "DriverGuid", "EstimatedEndTime", "StartGuid", "StartTime" },
                values: new object[] { new Guid("446d56eb-abed-43ea-bf45-bf197ec4ab76"), null, new Guid("30e1461d-61e5-40e2-bb3e-7a4b681c7b7b"), new Guid("c496ff01-5c7c-4296-9f3c-598b6e0d698c"), new Guid("bffa295b-ff0c-4c60-877b-2ca3b84d0a0e"), new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a48d8274-371d-4acc-b51f-3d973176359e"), new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Rides_AddressEntityId",
                table: "Rides",
                column: "AddressEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Addresses_AddressEntityId",
                table: "Rides",
                column: "AddressEntityId",
                principalTable: "Addresses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Addresses_DestinationGuid",
                table: "Rides",
                column: "DestinationGuid",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Addresses_AddressEntityId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Addresses_DestinationGuid",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_AddressEntityId",
                table: "Rides");

            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("446d56eb-abed-43ea-bf45-bf197ec4ab76"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("a48d8274-371d-4acc-b51f-3d973176359e"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("c496ff01-5c7c-4296-9f3c-598b6e0d698c"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("30e1461d-61e5-40e2-bb3e-7a4b681c7b7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bffa295b-ff0c-4c60-877b-2ca3b84d0a0e"));

            migrationBuilder.DropColumn(
                name: "AddressEntityId",
                table: "Rides");

            migrationBuilder.AddColumn<Guid>(
                name: "RideId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RideId",
                table: "Addresses",
                column: "RideId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Rides_RideId",
                table: "Addresses",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Addresses_DestinationGuid",
                table: "Rides",
                column: "DestinationGuid",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
