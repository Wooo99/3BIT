using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShareRide.DAL.Migrations
{
    public partial class FixRides : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Rides_RideEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RideEntityId",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("54753733-ff8e-47fb-befd-c36f549f31ae"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("7f5cf253-b20b-4f69-b0ae-4966680924c6"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("af0b0165-633c-44e4-90b2-7fc07b292743"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("bd2a594e-f484-45d8-81e9-e9a4cc665938"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f390282b-2fd9-488d-9397-9d715b6188bb"));

            migrationBuilder.DropColumn(
                name: "RideEntityId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "RideEntityUserEntity",
                columns: table => new
                {
                    PassengerRidesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PassengersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideEntityUserEntity", x => new { x.PassengerRidesId, x.PassengersId });
                    table.ForeignKey(
                        name: "FK_RideEntityUserEntity_Rides_PassengerRidesId",
                        column: x => x.PassengerRidesId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RideEntityUserEntity_Users_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("e448bf87-2c9a-4153-9ef2-85fb5dad9857"), "Brno", "Czech", null, "Kolejni 5", "61600" });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "RideId", "Street", "ZIP" },
                values: new object[] { new Guid("f41df063-904f-4ac2-8006-ca0963620966"), "Olomouc", "Czech", null, "Neredin 2", "779 00" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "PhotoPath" },
                values: new object[] { new Guid("b2000475-bea5-40a1-8998-7e5187a5c7a3"), "Adam", "Kovacik", null });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "CarType", "Manufacturer", "Name", "OwnerGuid", "PassengerSeats", "PhotoPath", "RegistrationYear" },
                values: new object[] { new Guid("5c7bcbea-09ba-48c8-b9d6-118683e7b9de"), 2, 48, "test name", new Guid("b2000475-bea5-40a1-8998-7e5187a5c7a3"), 4, null, 2000 });

            migrationBuilder.InsertData(
                table: "Rides",
                columns: new[] { "Id", "CarId", "DestinationGuid", "DriverGuid", "EstimatedEndTime", "StartGuid", "StartTime" },
                values: new object[] { new Guid("f88fd11b-0b03-4f7c-8e17-218fb1388230"), new Guid("5c7bcbea-09ba-48c8-b9d6-118683e7b9de"), new Guid("f41df063-904f-4ac2-8006-ca0963620966"), new Guid("b2000475-bea5-40a1-8998-7e5187a5c7a3"), new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), new Guid("e448bf87-2c9a-4153-9ef2-85fb5dad9857"), new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_RideEntityUserEntity_PassengersId",
                table: "RideEntityUserEntity",
                column: "PassengersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideEntityUserEntity");

            migrationBuilder.DeleteData(
                table: "Rides",
                keyColumn: "Id",
                keyValue: new Guid("f88fd11b-0b03-4f7c-8e17-218fb1388230"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("e448bf87-2c9a-4153-9ef2-85fb5dad9857"));

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: new Guid("f41df063-904f-4ac2-8006-ca0963620966"));

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: new Guid("5c7bcbea-09ba-48c8-b9d6-118683e7b9de"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2000475-bea5-40a1-8998-7e5187a5c7a3"));

            migrationBuilder.AddColumn<Guid>(
                name: "RideEntityId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

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
                name: "IX_Users_RideEntityId",
                table: "Users",
                column: "RideEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Rides_RideEntityId",
                table: "Users",
                column: "RideEntityId",
                principalTable: "Rides",
                principalColumn: "Id");
        }
    }
}
