using System;
using AutoMapper;
using ShareRide.Common;
using ShareRide.Common.Enums;
using ShareRide.DAL.Entities;

namespace ShareRide.BL.Models.DetailModels
{
    public record CarDetailModel : ModelBase
    {
        public Manufacturer Manufacturer { get; set; }
        public CarType CarType { get; set; }
        public int RegistrationYear { get; set; }
        public string Name { get; set; }
        public string? PhotoPath { get; set; }
        public int PassengerSeats { get; set; }
        public Guid? OwnerGuid { get; init; }

        public CarDetailModel() { }

        public CarDetailModel(Manufacturer Manufacturer, CarType CarType, int RegistrationYear, string Name, int PassengerSeats, Guid OwnerGuid)
        {
            this.Manufacturer = Manufacturer;
            this.CarType = CarType;
            this.RegistrationYear = RegistrationYear;
            this.Name = Name;
            this.PassengerSeats = PassengerSeats;
            this.OwnerGuid = OwnerGuid;
        }

        public class CarMapperProfile : Profile
        {
            public CarMapperProfile()
            {
                CreateMap<CarEntity, CarDetailModel>()
                    .ForMember(dst => dst.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer))
                    .ForMember(dst => dst.OwnerGuid, opt => opt.MapFrom(src => src.OwnerGuid))
                    .ReverseMap();
            }
        }

        public static CarDetailModel Empty => new(default, default, int.MinValue, string.Empty, int.MinValue, Guid.Empty);
    }
}