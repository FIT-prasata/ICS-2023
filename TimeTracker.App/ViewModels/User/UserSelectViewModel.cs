using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.User
{
    public partial class UserSelectViewModel: ViewModelBase
    {
        private readonly  IUserFacade _userFacade;
        private readonly  IActiveUserService _activeUserService;
        private readonly  INavigationService _navigationService;

        public IEnumerable<UserListModel> Users { get; set; } = new List<UserListModel>();

        public UserSelectViewModel(IUserFacade userFacade, IActiveUserService activeUserService, INavigationService navigationService, IMessengerService messengerService): base(messengerService)
        {
            _userFacade = userFacade;
            _activeUserService = activeUserService;
            _navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();
            Users = await _userFacade.GetAsync();
        }

        [RelayCommand]
        private async Task UserSelectedAsync(Guid id)
        {
            _activeUserService.SetId(id);
            MessengerService.Send(new Messages.UserAuthenticatedMessage());
            await _navigationService.GoToAsync<ProjectListViewModel>();
        }
    }
}
