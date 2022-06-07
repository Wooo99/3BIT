using ShareRide.App.Factories;
using ShareRide.App.ViewModels;
using ShareRide.App.Messages;
using ShareRide.App.Services;
using ShareRide.App.Services.MessageDialog;
using ShareRide.App.Wrappers;
using ShareRide.BL.Facades;
using ShareRide.BL.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ShareRide.App.Commands;
using System.Threading.Tasks;

namespace ShareRide.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Readonly
        private readonly IMediator _mediator;
        private readonly IMessageDialogService _messageDialogService;
        
        //Commands
        public ICommand logoutCommand { get; }
        
        //Facades and wrappers
        public UserFacade UserFacade { get; set; }
        public UserWrapper userWrapper { get; set; }

        //ViewModels
        //
        public IRideListViewModel? RideListViewModel { get; set; }

        public LoginViewModel? LoginViewModel { get; private set; }
        private Views.LoginWindow? _loginWindow;

        public MainViewModel(
            IMediator mediator,
            IMessageDialogService messageDialogService,
            IRideListViewModel RideListViewModel,
            UserFacade userFacade)
        {
            this._mediator = mediator;
            this._messageDialogService = messageDialogService;
            this.UserFacade = userFacade;
            this.RideListViewModel = RideListViewModel;

            userWrapper = BL.Models.DetailModels.UserDetailModel.Empty;

            //Commands
            //Logout command
            logoutCommand = new AsyncRelayCommand(Logout);
            //login message
            _mediator.Register<SelectedMessage<UserWrapper>>(Login);
        }

        public async Task Logout()
        {
            userWrapper = BL.Models.DetailModels.UserDetailModel.Empty;
            LoginViewModel = new LoginViewModel(_mediator, _messageDialogService, UserFacade);

            _loginWindow = new ShareRide.App.Views.LoginWindow(LoginViewModel);
            _loginWindow.ShowDialog();
        }

        private async void Login(SelectedMessage<UserWrapper> message)
        {
            userWrapper = await UserFacade.GetAsync((Guid)(message.Id!)) ?? BL.Models.DetailModels.UserDetailModel.Empty;
            if(_loginWindow != null)
            {
                _loginWindow.Close();
            }
        }
    }
}