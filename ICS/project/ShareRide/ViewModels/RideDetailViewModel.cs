using ShareRide.App.Messages;
using ShareRide.App.Services;
using ShareRide.App.Services.MessageDialog;
using ShareRide.App.Wrappers;
using ShareRide.BL.Models.DetailModels;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ShareRide.App.Commands;
using ShareRide.BL.Facades;
using ShareRide.Common.Enums;
using ShareRide.BL.Models.ListModels;

namespace ShareRide.App.ViewModels
{
    public class RideDetailViewModel : ViewModelBase, IRideDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;
        
        private readonly RideFacade _rideFacade;
        private readonly UserFacade _userFacade;

        private RideWrapper? _model = RideDetailModel.Empty;
        private UserWrapper? _userWrapper = UserDetailModel.Empty;
        public RideWrapper? Model
        {
            get => _model;
            set
            {
                _model = value;
            }
        }

        public ICommand DeleteCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand KickUserCommand { get; }


        public RideDetailViewModel(
            RideFacade rideFacade,
            UserFacade userFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _messageDialogService = messageDialogService;
            _mediator = mediator;
            _userFacade = userFacade;
            _rideFacade = rideFacade;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
            KickUserCommand = new AsyncRelayCommand<Guid>(KickUser);

            _mediator.Register<UpdateMessage<RideWrapper>>(RideUpdated);

        }

        private async void RideUpdated(UpdateMessage<RideWrapper> model) => await LoadAsync((Guid)model.Id);
        public async Task LoadAsync(Guid id) => Model = await _rideFacade.GetAsync(id) ?? RideDetailModel.Empty;

        public async Task DeleteAsync()
        {
            if (Model is null)
            {
                throw new InvalidOperationException("Null model cannot be deleted");
            }

            if (Model.Id != Guid.Empty)
            {
                var delete = _messageDialogService.Show(
                    "Delete",
                    $"Do you want to delete {Model?.Start}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No)
                {
                    return;
                }

                try
                {
                    await _rideFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.Start} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<RideWrapper> { Model = Model! });
            }
        }

        public async Task KickUser(Guid id)
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }
            _userWrapper = await _userFacade.GetAsync(id) ?? UserDetailModel.Empty;
            if (_userWrapper.Id != id)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }
            Model.Passengers.Remove(_userWrapper);
            await SaveAsync();
        }

        private bool CanSave() => Model?.IsValid ?? false;

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _rideFacade.SaveAsync(Model);
            _mediator.Send(new UpdateMessage<RideWrapper> { Model = Model });
        }

        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
            Model = new RideWrapper(new RideDetailModel(
                Start: "",
                Destination: "",
                StartTime: DateTime.Now,
                EstimatedEndTime: DateTime.Now.AddHours(1),
                Driver: UserDetailModel.Empty,
                Car: CarDetailModel.Empty
                ));
        }
    }
}