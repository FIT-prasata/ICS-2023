using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.User;

public partial class UserDetailViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;


    public IEnumerable<UserListModel> Users { get; private set; }

    public UserDetailViewModel(
            IUserFacade userFacade,
            INavigationService navigationService,
            IMessengerService messengerService,
            IAlertService alertService
        )
        : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Users = await _userFacade.GetAsync();
    }

}