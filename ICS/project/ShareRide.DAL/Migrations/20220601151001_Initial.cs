using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RideId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Manufacturer = table.Column<int>(type: "int", nullable: false),
                    CarType = table.Column<int>(type: "int", nullable: false),
                    RegistrationYear = table.Column<int>(type: "int", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassengerSeats = table.Column<int>(type: "int", nullable: false),
                    OwnerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DestinationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DriverGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rides_Addresses_DestinationGuid",
                        column: x => x.DestinationGuid,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rides_Addresses_StartGuid",
                        column: x => x.StartGuid,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rides_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RideEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Rides_RideEntityId",
                        column: x => x.RideEntityId,
                        principalTable: "Rides",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("7f5cf253-b20b-4f69-b0ae-4966680924c6"), "Brno", "Czech", null, "Kolejni 5", "61600" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("af0b0165-633c-44e4-90b2-7fc07b292743"), "Olomouc", "Czech", null, "Neredin 2", "779 00" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PhotoPath", "RideEntityId" },
                values: new object[] { new Guid("f390282b-2fd9-488d-9397-9d715b6188bb"), "Adam", "Kovacik", null, null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "Manufacturer", "Name", "OwnerGuid", "PassengerSeats", "PhotoPath", "RegistrationYear" },
                values: new object[] { new Guid("bd2a594e-f484-45d8-81e9-e9a4cc665938"), 2, 48, "test name", new Guid("f390282b-2fd9-488d-9397-9d715b6188bb"), 4, null, 2000 });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "CarId", "DestinationGuid", "DriverGuid", "EstimatedEndTime", "StartGuid", "StartTime" },
                values: new object[] { new Guid("54753733-ff8e-47fb-befd-c36f549f31ae"), new Guid("bd2a594e-f484-45d8-81e9-e9a4cc665938"), new Guid("af0b0165-633c-44e4-90b2-7fc07b292743"), new Guid("f390282b-2fd9-488d-9397-9d715b6188bb"), new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("7f5cf253-b20b-4f69-b0ae-4966680924c6"), new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_RideId",
                table: "Addresses",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_OwnerGuid",
                table: "Cars",
                column: "OwnerGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_CarId",
                table: "Rides",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DestinationGuid",
                table: "Rides",
                column: "DestinationGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_DriverGuid",
                table: "Rides",
                column: "DriverGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_StartGuid",
                table: "Rides",
                column: "StartGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RideEntityId",
                table: "Users",
                column: "RideEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Rides_RideId",
                table: "Addresses",
                column: "RideId",
                principalTable: "Rides",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_OwnerGuid",
                table: "Cars",
                column: "OwnerGuid",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Addresses_Rides_RideId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rides_RideEntityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Rides");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
