using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.DAL.Migrations
{
    public partial class RemoveAdress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Addresses_AddressEntityId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Addresses_DestinationGuid",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Addresses_StartGuid",
                table: "Rides");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Rides_AddressEntityId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_DestinationGuid",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_StartGuid",
                table: "Rides");

            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("446d56eb-abed-43ea-bf45-bf197ec4ab76"));

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

            migrationBuilder.DropColumn(
                name: "DestinationGuid",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "StartGuid",
                table: "Rides");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Start",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PhotoPath" },
                values: new object[] { new Guid("1346f46f-f62c-471a-bbc6-3b5b7a514281"), "Adam", "Kovacik", null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "Manufacturer", "Name", "OwnerGuid", "PassengerSeats", "PhotoPath", "RegistrationYear" },
                values: new object[] { new Guid("2dbd3cd2-ac74-45d0-9b47-8e268090e29b"), 2, 48, "test name", new Guid("1346f46f-f62c-471a-bbc6-3b5b7a514281"), 4, null, 2000 });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "CarId", "Destination", "DriverGuid", "EstimatedEndTime", "Start", "StartTime" },
                values: new object[] { new Guid("8cd12cb6-0de2-4fb9-8ffe-76f96967ab97"), new Guid("2dbd3cd2-ac74-45d0-9b47-8e268090e29b"), "Slavkov", new Guid("1346f46f-f62c-471a-bbc6-3b5b7a514281"), new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "Brno", new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("8cd12cb6-0de2-4fb9-8ffe-76f96967ab97"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("2dbd3cd2-ac74-45d0-9b47-8e268090e29b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1346f46f-f62c-471a-bbc6-3b5b7a514281"));

            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Rides");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressEntityId",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DestinationGuid",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StartGuid",
                table: "Rides",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DestinationGuid",
                table: "Rides",
                column: "DestinationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_StartGuid",
                table: "Rides",
                column: "StartGuid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Addresses_StartGuid",
                table: "Rides",
                column: "StartGuid",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
