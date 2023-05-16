using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.Tokens;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Enums;

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

    public List<ActivityType> ActivityTypes { get; set; }

    public DateTime DateStart {get; set; } = DateTime.Now;
    public TimeSpan TimeStart { get; set; } = new TimeSpan(0, 0, 0);

    public DateTime DateEnd { get; set; } = DateTime.Now;
    public TimeSpan TimeEnd { get; set; } = new TimeSpan(0, 0, 0);

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

        ActivityTypes = Enum.GetValues<ActivityType>().Where(a => a != ActivityType.Empty).ToList();

        
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        if (ActivityId == Guid.Empty)
        {
            Activity = ActivityDetailModel.Empty;
        }
        else
        {
            Activity = await _activityFacade.GetAsync(ActivityId);
            DateStart = Activity.Start;
            TimeStart = Activity.Start.TimeOfDay;
            DateEnd = Activity.End;
            TimeEnd = Activity.End.TimeOfDay;

        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Activity.Type == ActivityType.Empty)
        {
            await _alertService.DisplayAsync("Error", "Type is required");
            return;
        }
        if (DateEnd < DateStart)
        {
            await _alertService.DisplayAsync("Error", "End date can't be before start date");
            return;
        }

        if (DateEnd.Date == DateStart.Date && TimeEnd <= TimeStart)
        {
            await _alertService.DisplayAsync("Error", "End time can't be before start time or at the same time");
            return;
        }

        Activity.Start = DateStart.Date + TimeStart;
        Activity.End = DateEnd.Date + TimeEnd;
        try
        {
            await _activityFacade.SaveAsync(Activity!);
        }
        catch (SecurityTokenException e)
        {
            await _alertService.DisplayAsync("Error", e.Message);
        }
        MessengerService.Send(new ActivityEditMessage { ActivityId = ActivityId });
        await LoadDataAsync();
        await _alertService.DisplayAsync("Success!", "Activity changes were saved.");
    }


}
