using ShareRide.BL.Models.DetailModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using ShareRide.App.Extensions;
using ShareRide.Common.Enums;

namespace ShareRide.App.Wrappers
{
    public class UserWrapper : ModelWrapper<UserDetailModel>
    {
        public UserWrapper(UserDetailModel model)
            : base(model)
        {
        }
        public string? FirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? LastName
        {
            get => GetValue<String>();
            set => SetValue(value);
        }
        public string? PhotoPath
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public ObservableCollection<CarWrapper> OwnedCars { get; set; } = new();

        public static implicit operator UserWrapper(UserDetailModel detailModel)
                   => new(detailModel);

        public static implicit operator UserDetailModel(UserWrapper wrapper)
            => wrapper.Model;
    }
}