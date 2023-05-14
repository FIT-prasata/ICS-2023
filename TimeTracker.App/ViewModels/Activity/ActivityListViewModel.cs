using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Activity;
public partial class ActivityListViewModel: ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;

    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;

    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = await _activityFacade.GetAsync();
    }
}
