using System;
using AutoMapper;
using ShareRide.Common;
using ShareRide.Common.Enums;
using ShareRide.DAL.Entities;

namespace ShareRide.BL.Models.ListModels
{
    public record CarListModel : ModelBase
    {
        public Manufacturer Manufacturer { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int PassengerSeats { get; private set; }

        public CarListModel() { }

        public CarListModel(string Name)
        {
            this.Name = Name;
        }

        public class CarMapperProfile : Profile
        {
            public CarMapperProfile()
            {
                CreateMap<CarEntity, CarListModel>()
                    .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dst => dst.Manufacturer, opt => opt.MapFrom(src => src.Manufacturer))
                    .ForMember(dst => dst.ImageUrl, opt => opt.MapFrom(src => src.PhotoPath))
                    .ForMember(dst => dst.PassengerSeats, opt => opt.MapFrom(src => src.PassengerSeats))
                    .ReverseMap();
            }
        }
    }
}