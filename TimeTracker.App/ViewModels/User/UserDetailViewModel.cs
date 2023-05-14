using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.User;

[QueryProperty(nameof(UserId), nameof(UserId))]
public partial class UserDetailViewModel : ViewModelBase
{
    private readonly UserFacade _userFacade;
    private readonly INavigationService _navigationService;
    private readonly IAlertService _alertService;

    public Guid UserId { get; set; }

    public UserDetailModel? User { get; private set; }

    public UserDetailViewModel(
            UserFacade userFacade,
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

        User = await _userFacade.GetAsync(UserId);
    }

}