using System;
using System.Collections.Generic;
using AutoMapper;
using ShareRide.DAL.Entities;

namespace ShareRide.BL.Models.DetailModels
{
    public record UserDetailModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoPath { get; set; }
        public ICollection<CarDetailModel>? OwnedCars { get; init; }

        public UserDetailModel() { }

        public UserDetailModel(string FirstName, string LastName, string PhotoPath = "")
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhotoPath = PhotoPath;
        }

        public class UserMapperProfile : Profile
        {
            public UserMapperProfile()
            {
                CreateMap<UserEntity, UserDetailModel>()
                    .ReverseMap();
            }
        }

        public static UserDetailModel Empty => new(string.Empty, string.Empty, string.Empty);
    }
}