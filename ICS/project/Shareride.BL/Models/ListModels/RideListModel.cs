using System;
using System.Collections.Generic;
using AutoMapper;
using ShareRide.DAL.Entities;

namespace ShareRide.BL.Models.ListModels
{
    public record RideListModel : ModelBase
    {
        public CarListModel Car { get; set; }
        public UserListModel Driver { get; set; }
        public string FromCity { get; set; }
        public string ToCity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EstimatedEndTime { get; set; }
        public ICollection<UserListModel>? Passengers { get; set; }

        public RideListModel() { }

        public RideListModel(CarListModel Car ,UserListModel Driver ,string From, string To ,DateTime Start, DateTime EstimatedEnd, ICollection<UserListModel>? Passengers)
        {
            this.Car = Car;
            this.Driver = Driver;
            this.FromCity = From;
            this.ToCity = To;
            this.StartTime = Start;
            this.EstimatedEndTime = EstimatedEnd;
            this.Passengers = Passengers;
        }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<RideEntity, RideListModel>()
                    .ForMember(dst => dst.FromCity, opt => opt.MapFrom(src => src.Start))
                    .ForMember(dst => dst.ToCity, opt => opt.MapFrom(src => src.Destination))
                    .ForMember(dst => dst.StartTime, opt => opt.MapFrom(src => src.StartTime))
                    .ForMember(dst => dst.EstimatedEndTime, opt => opt.MapFrom(src => src.EstimatedEndTime));
            }
        }
    }
}