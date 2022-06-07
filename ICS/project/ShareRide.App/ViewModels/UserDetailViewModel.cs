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
    public class UserDetailViewModel : ViewModelBase, IUserDetailViewModel
    {
        private readonly IMediator _mediator;
        private readonly UserFacade _userFacade;
        private readonly IMessageDialogService _messageDialogService;

        public UserDetailViewModel(
            UserFacade userFacade,
            IMessageDialogService messageDialogService,
            IMediator mediator)
        {
            _userFacade = userFacade;
            _messageDialogService = messageDialogService;
            _mediator = mediator;

            SaveCommand = new AsyncRelayCommand(SaveAsync, CanSave);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        public UserWrapper? Model { get; private set; }
        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }


        public async Task LoadAsync(Guid id)
        {
            Model = await _userFacade.GetAsync(id) ??  UserDetailModel.Empty;
        }

        public async Task SaveAsync()
        {
            if (Model == null)
            {
                throw new InvalidOperationException("Null model cannot be saved");
            }

            Model = await _userFacade.SaveAsync(Model.Model);
            _mediator.Send(new UpdateMessage<UserWrapper> { Model = Model });
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
                    $"Do you want to delete {Model?.FirstName}?.",
                    MessageDialogButtonConfiguration.YesNo,
                    MessageDialogResult.No);

                if (delete == MessageDialogResult.No) return;

                try
                {
                    await _userFacade.DeleteAsync(Model!.Id);
                }
                catch
                {
                    var _ = _messageDialogService.Show(
                        $"Deleting of {Model?.FirstName} failed!",
                        "Deleting failed",
                        MessageDialogButtonConfiguration.OK,
                        MessageDialogResult.OK);
                }

                _mediator.Send(new DeleteMessage<UserWrapper>
                {
                    Model = Model
                });
            }
        }

        public override void LoadInDesignMode()
        {
            base.LoadInDesignMode();
            Model = new UserWrapper(new UserDetailModel(
                FirstName: "Jan",
                LastName: "Pluskal")
            {
                PhotoPath = "https://www.fit.vut.cz/person-photo/128754/?transparent=1"
            });
        }
    }
}