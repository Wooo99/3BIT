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
    public class CarWrapper : ModelWrapper<CarDetailModel>
    {
        public CarWrapper(CarDetailModel model)
            : base(model)
        {
        }
        public Manufacturer Manufacturer
        {
            get => GetValue<Manufacturer>();
            set => SetValue(value);
        }
        public CarType CarType
        {
            get => GetValue<CarType>();
            set => SetValue(value);
        }
        public int RegistrationYear
        {
            get => GetValue<int>();
            set => SetValue(value);
        }
        public string? Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public string? PhotoPath
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public int PassengerSeats
        {
            get => GetValue<int>();
            set => SetValue(value);
        }
        public Guid OwnerGuid
        {
            get => GetValue<Guid>();
            set => SetValue(value);
        }
        public string? OwnerFirstName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        public static implicit operator CarWrapper(CarDetailModel detailModel)
                   => new(detailModel);

        public static implicit operator CarDetailModel(CarWrapper wrapper)
            => wrapper.Model;
    }
}