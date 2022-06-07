﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShareRide.DAL;

#nullable disable

namespace ShareRide.DAL.Migrations
{
    [DbContext(typeof(ShareRideDbContext))]
    [Migration("20220602140708_SetOnDeleteActions")]
    partial class SetOnDeleteActions
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RideEntityUserEntity", b =>
                {
                    b.Property<Guid>("PassengerRidesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PassengersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PassengerRidesId", "PassengersId");

                    b.HasIndex("PassengersId");

                    b.ToTable("RideEntityUserEntity");
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.AddressEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RideId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZIP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RideId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a7f74cd2-0bcd-4b66-8d26-1e7ad7333bed"),
                            City = "Brno",
                            Country = "Czech",
                            Street = "Kolejni 5",
                            ZIP = "61600"
                        },
                        new
                        {
                            Id = new Guid("a61c020d-401e-43da-8053-6eaa27dda904"),
                            City = "Olomouc",
                            Country = "Czech",
                            Street = "Neredin 2",
                            ZIP = "779 00"
                        });
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.CarEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CarType")
                        .HasColumnType("int");

                    b.Property<int>("Manufacturer")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PassengerSeats")
                        .HasColumnType("int");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RegistrationYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OwnerGuid");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("30f2b280-90f8-4902-b5da-40619242a208"),
                            CarType = 2,
                            Manufacturer = 48,
                            Name = "test name",
                            OwnerGuid = new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"),
                            PassengerSeats = 4,
                            RegistrationYear = 2000
                        });
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.RideEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DestinationGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DriverGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EstimatedEndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("StartGuid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("DestinationGuid");

                    b.HasIndex("DriverGuid");

                    b.HasIndex("StartGuid");

                    b.ToTable("Rides");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b3921442-5f51-49db-9518-b91436733b33"),
                            CarId = new Guid("30f2b280-90f8-4902-b5da-40619242a208"),
                            DestinationGuid = new Guid("a61c020d-401e-43da-8053-6eaa27dda904"),
                            DriverGuid = new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"),
                            EstimatedEndTime = new DateTime(2022, 4, 10, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartGuid = new Guid("a7f74cd2-0bcd-4b66-8d26-1e7ad7333bed"),
                            StartTime = new DateTime(2022, 4, 10, 8, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("7f6f7426-13a2-4396-a208-62185e32fb0f"),
                            FirstName = "Adam",
                            LastName = "Kovacik"
                        });
                });

            modelBuilder.Entity("RideEntityUserEntity", b =>
                {
                    b.HasOne("ShareRide.DAL.Entities.RideEntity", null)
                        .WithMany()
                        .HasForeignKey("PassengerRidesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShareRide.DAL.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("PassengersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.AddressEntity", b =>
                {
                    b.HasOne("ShareRide.DAL.Entities.RideEntity", "Ride")
                        .WithMany()
                        .HasForeignKey("RideId");

                    b.Navigation("Ride");
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.CarEntity", b =>
                {
                    b.HasOne("ShareRide.DAL.Entities.UserEntity", "Owner")
                        .WithMany("OwnedCars")
                        .HasForeignKey("OwnerGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.RideEntity", b =>
                {
                    b.HasOne("ShareRide.DAL.Entities.CarEntity", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("ShareRide.DAL.Entities.AddressEntity", "Destination")
                        .WithMany()
                        .HasForeignKey("DestinationGuid")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ShareRide.DAL.Entities.UserEntity", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverGuid");

                    b.HasOne("ShareRide.DAL.Entities.AddressEntity", "Start")
                        .WithMany()
                        .HasForeignKey("StartGuid")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Car");

                    b.Navigation("Destination");

                    b.Navigation("Driver");

                    b.Navigation("Start");
                });

            modelBuilder.Entity("ShareRide.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("OwnedCars");
                });
#pragma warning restore 612, 618
        }
    }
}
