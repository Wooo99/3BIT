using ShareRide.App.Messages;
using ShareRide.App.Services;
using ShareRide.App.Services.MessageDialog;
using ShareRide.App.Wrappers;
using ShareRide.BL.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ShareRide.App.Commands;
using ShareRide.BL.Facades;
using ShareRide.BL.Models.DetailModels;

namespace ShareRide.App.ViewModels
{
    public class CarDetailViewModel : ViewModelBase, ICarDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly CarFacade _carFacade;
        private readonly IMessageDialogService _messageDialogService;

        public CarDetailViewModel(
            CarFacade carFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _carFacade = carFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        public CarWrapper? Model { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            //Model = await _carFacade.GetAsync(id) ??  CarDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _carFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<CarWrapper> { Model = Model });
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    $"Delete",
                    $"Do you want to delete {Model?.Name}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    await _carFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.Name} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<CarWrapper>
                {
                    Model = Model
                });
            }
        }

        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
           /* Model = new CarWrapper(new CarDetailModel(
                Manufacturer: Common.Enums.Manufacturer.Dodge,
                CarType: Common.Enums.CarType.Sedan,
                RegistrationYear: 1970,
                Name: "Moje_Auticko",
                PassengerSeats: 4,
                OwnerFirstName: "Jan"));
           */
        }
    }
}