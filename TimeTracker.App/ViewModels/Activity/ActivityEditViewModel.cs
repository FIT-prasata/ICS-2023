using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Activity;

[QueryProperty(nameof(ActivityId), nameof(ActivityId))]
    public partial class ActivityEditViewModel: ViewModelBase
    {
        private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;
    private readonly IAlertService _alertService;

    public Guid ActivityId { get; set; }

    public ActivityDetailModel? Activity { get; set; }

    public ActivityEditViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IActiveUserService activeUserService,
        IAlertService alertService,
        IMessengerService messengerService
    ) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _activeUserService = activeUserService;
        _alertService = alertService;
    }
}
