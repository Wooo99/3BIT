using System;
using ShareRide.App.Extensions;
using ShareRide.App.Messages;
using ShareRide.App.Services;
using ShareRide.App.Wrappers;
using ShareRide.BL.Models;
using ShareRide.BL.Models.DetailModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ShareRide.App.Commands;
using ShareRide.BL.Facades;
using ShareRide.Common.Enums;
using ShareRide.BL.Models.ListModels;

namespace ShareRide.App.ViewModels
{
    internal class RideListViewModel : ViewModelBase, IRideListViewModel
    {
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;
        private readonly IMediator _mediator;

        public UserDetailModel? User { get; set; }
        public ObservableCollection<RideListModel> PrivateRides { get; set; } = new();
        public ObservableCollection<RideListModel> PublicRides { get; set; } = new();
        public int IsDriver { get; set; }

        //Filters
        public string? StartFilterString { get; set; } = ".*";
        public string? DestinationFilterString { get; set; } = ".*";
        public DateTime? StartDateFilter { get; set; } = DateTime.Now;

        //Commands
        //Used to Book/unbook rides
        public ICommand RideBookCommand { get; set; }
        //Used to cancel rides
        public ICommand RideDetailsCommand { get; set; }
        
        public ICommand ResetFilters { get; set; }
        public ICommand SetFilters { get; set; }


        public RideListViewModel(RideFacade rideFacade, UserFacade userFacade, IMediator mediator)
        {
            _rideFacade = rideFacade;
            _userFacade = userFacade;
            _mediator = mediator;

            User = UserDetailModel.Empty;

            RideBookCommand = new AsyncRelayCommand<Guid>(BookRide);
            RideDetailsCommand = new AsyncRelayCommand<Guid>(RideDetails);
            ResetFilters = new AsyncRelayCommand(FilterReset);
            SetFilters = new AsyncRelayCommand(LoadAsync);

            _mediator.Register<DeleteMessage<RideWrapper>>(RideDeleted);
            _mediator.Register<UpdateMessage<RideWrapper>>(RideUpdated);
            _mediator.Register<AddedMessage<RideWrapper>>(RideCreated);
            _mediator.Register<SelectedMessage<UserWrapper>>(UserSelected);
        }

        private async Task BookRide(Guid id)
        {
            BL.Models.DetailModels.RideDetailModel? rideToBook = await _rideFacade.GetAsync(id);
            bool contained = false;


            if(rideToBook != null)
            {
                foreach(var user in rideToBook.Passengers)
                {
                    if(user.Id == User.Id)
                    {
                        contained = true; 
                        break;
                    }
                }

                if (contained){
                    //TODO Cant remove user
                    rideToBook.Passengers.Remove(User);
                    await _rideFacade.SaveAsync(rideToBook);
                    await LoadAsync();
                } else
                {
                    if (rideToBook.Passengers.Count < rideToBook.Car.PassengerSeats)
                    {
                        rideToBook.Passengers.Add(User);
                        await _rideFacade.SaveAsync(rideToBook);
                        await LoadAsync();
                    }
                }
            }
        }

        private async Task RideDetails(Guid id)
        {

        }

        private async Task FilterReset()
        {
            StartFilterString = ".*";
            DestinationFilterString = ".*";
            StartDateFilter = DateTime.Now;
            await LoadAsync();
        }

        private async void UserSelected(SelectedMessage<UserWrapper> message)
        {
            User = await _userFacade.GetAsync((Guid)(message.Id!)) ?? BL.Models.DetailModels.UserDetailModel.Empty;
            await LoadAsync();
        }

        private async void RideDeleted(DeleteMessage<RideWrapper> _) => await LoadAsync();

        private async void RideUpdated(UpdateMessage<RideWrapper> _) => await LoadAsync();

        private async void RideCreated(AddedMessage<RideWrapper> _) => await LoadAsync();

        public async Task LoadAsync()
        {
            PublicRides.Clear();
            var PubRides = await _rideFacade.GetAsync(id: User.Id, isDriver: 1, fromCity: StartFilterString!, toCity: DestinationFilterString!, startTime: StartDateFilter);
            PublicRides.AddRange(PubRides);
            
            PrivateRides.Clear();
            var PriRides = await _rideFacade.GetAsync(id: User.Id, isDriver: 0, fromCity: StartFilterString!, toCity: DestinationFilterString!, startTime: StartDateFilter);
            PrivateRides.AddRange(PriRides);
        }

        public override void LoadInDesignMode()
        {
            /*
            Rides.Add( new RideListModel(
                FromCity: "Brno",
                ToCity: "Bratislava",
                StartTime: DateTime.UtcNow
                ));
        */
        }
    }
}
