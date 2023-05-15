using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.User;

public partial class UserDetailViewModel : ViewModelBase
{
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;
    private readonly IActiveUserService _activeUserService;


    public UserDetailModel? User { get; private set; }
    public IEnumerable<ActivityListModel> UserActivities { get; set; } = null!;

    public UserDetailViewModel(
            IUserFacade userFacade,
            IActivityFacade activityFacade,
            INavigationService navigationService,
            IActiveUserService activeUserService,
            IMessengerService messengerService,
            IAlertService alertService
        )
        : base(messengerService)
    {
        _userFacade = userFacade;
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _alertService = alertService;
        _activeUserService = activeUserService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        User = await _userFacade.GetAsync(_activeUserService.GetId());
        UserActivities = await _activityFacade.GetActivitiesByUserAssignedAsync(_activeUserService.GetId());
    }

}