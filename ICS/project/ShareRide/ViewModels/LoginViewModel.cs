using ShareRide.App.Factories;
using ShareRide.App.Messages;
using ShareRide.App.Services;
using ShareRide.App.Services.MessageDialog;
using ShareRide.App.Wrappers;
using ShareRide.BL.Facades;
using ShareRide.BL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ShareRide.App.Commands;
using System.Threading.Tasks;
using System.Windows;

namespace ShareRide.App.ViewModels
{
    public class LoginViewModel : ViewModelBase, IListViewModel
    {
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;
        public ObservableCollection<BL.Models.ListModels.UserListModel>? Users { get; set; } = new ObservableCollection<BL.Models.ListModels.UserListModel>();
        public UserFacade _userFacade { get; set; }
        public UserWrapper selectedUser { get; set; }
        public UserWrapper newUser { get; set; }

        public int selectedUserIndex { get; set; } = -1;
        
        public ICommand LoginCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }

        public LoginViewModel(
            IMediator mediator,
            IMessageDialogService messageDialogService,
            UserFacade userFacade)
        {
            _mediator = mediator;
            _messageDialogService = messageDialogService;
            _userFacade = userFacade;
            _messageDialogService = messageDialogService;
            _userFacade = userFacade;

            newUser = BL.Models.DetailModels.UserDetailModel.Empty;

            LoginCommand = new RelayCommand(Login);
            CreateUserCommand = new RelayCommand(UserCreated ,UserIsValid);
        }

        private async void UserCreated()
        {
            if(_userFacade != null)
            {
                await _userFacade.SaveAsync(newUser);
            }
            await LoadAsync();
        }

        private bool UserIsValid() => newUser?.IsValid ?? false;

        private async void Login()
        {
            var id = Users[selectedUserIndex].Id;
            selectedUser = await _userFacade.GetAsync(id) ?? BL.Models.DetailModels.UserDetailModel.Empty;
            _mediator.Send(new SelectedMessage<UserWrapper>() 
                { 
                    Id = selectedUser.Id,
                    Model = selectedUser
                }
            );
        }

        public async Task LoadAsync()
        {
            if(Users != null)
            {
                Users.Clear();
            }
            var _users = await _userFacade.GetAsync();
            foreach(var user in _users)
            {
                Users.Add(user);
            }
        }
    }
}
