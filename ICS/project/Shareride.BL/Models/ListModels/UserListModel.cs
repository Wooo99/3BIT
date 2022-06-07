using System;
using System.Collections.Generic;
using AutoMapper;
using ShareRide.DAL.Entities;

namespace ShareRide.BL.Models.ListModels
{
    public record UserListModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? PhotoPath { get; set; }

        public UserListModel() { }

        public UserListModel(string FirstName, string SecondName, string PhotoPath = "")
        {
            this.FirstName = FirstName;
            this.LastName = SecondName;
            this.PhotoPath = PhotoPath;
        }

        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<UserEntity, UserListModel>();
            }
        }

        public static UserListModel Empty => new(String.Empty, String.Empty, string.Empty); 
    }
}