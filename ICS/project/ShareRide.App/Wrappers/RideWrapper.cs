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
    public class RideWrapper : ModelWrapper<RideDetailModel>
    {
        public RideWrapper(RideDetailModel model)
            : base(model)
        {
        }
        public String Start
        {
            get => GetValue<String>()!;
            set => SetValue(value);
        }
        public String Destination
        {
            get => GetValue<String>()!;
            set => SetValue(value);
        }
        public DateTime StartTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public DateTime EstimatedEndTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
        public CarDetailModel? Car
        {
            get => GetValue<CarDetailModel>();
            set => SetValue(value);
        }
        public ObservableCollection<UserWrapper> Passengers { get; set; } = new();

        public static implicit operator RideWrapper(RideDetailModel detailModel)
                   => new(detailModel);

        public static implicit operator RideDetailModel(RideWrapper wrapper)
            => wrapper.Model;
    }
}