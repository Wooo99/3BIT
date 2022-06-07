using System;
using System.Collections.Generic;
using AutoMapper;
using ShareRide.DAL.Entities;

namespace ShareRide.BL.Models.DetailModels
{
    public record RideDetailModel : ModelBase
    {
        public String Start { get; set; }
        public String Destination { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EstimatedEndTime { get; set; }
        public UserDetailModel Driver { get; set; }
        public ICollection<UserDetailModel>? Passengers { get; init; }
        public CarDetailModel? Car { get; set; }

        public RideDetailModel() { }

        public RideDetailModel(String Start, String Destination, DateTime StartTime,
            DateTime EstimatedEndTime, UserDetailModel Driver, CarDetailModel Car)
        {
            this.Start = Start;
            this.Destination = Destination;
            this.StartTime = StartTime;
            this.EstimatedEndTime = EstimatedEndTime;
            this.Driver = Driver;
            this.Car = Car;
        }

        public class RideMapperProfile : Profile
        {
            public RideMapperProfile()
            {
                CreateMap<RideEntity, RideDetailModel>()
                    .ForMember(dst => dst.Start, opt => opt.MapFrom(src => src.Start))
                    .ForMember(dst => dst.Destination, opt => opt.MapFrom(src => src.Destination))
                    .ForMember(dst => dst.StartTime, opt => opt.MapFrom(src => src.StartTime))
                    .ForMember(dst => dst.EstimatedEndTime, opt => opt.MapFrom(src => src.EstimatedEndTime))
                    .ForMember(dst => dst.Driver, opt => opt.MapFrom(src => src.Driver))
                    .ForMember(dst => dst.Passengers, opt => opt.MapFrom(src => src.Passengers))
                    .ForMember(dst => dst.Car, opt => opt.MapFrom(src => src.Car))
                    .ReverseMap();
            }
        }
        public static RideDetailModel Empty => new(String.Empty, String.Empty, default, default, default, CarDetailModel.Empty);
    }
}