using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.User;

public partial class UserDetailViewModel : ViewModelBase, IRecipient<ActivityAddMessage>, IRecipient<ActivityDeleteMessage>
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

    [RelayCommand]
    private async Task SaveUserChangesAsync()
    {
        if (User.FirstName == string.Empty || User.LastName == string.Empty || User.ImgUri == string.Empty)
        {
            await _alertService.DisplayAsync("Error", "Name, Surname or Image URL cannot be empty");
            return;
        }
        await _alertService.DisplayAsync("Success!", "User changes were saved.");
        await _userFacade.SaveAsync(User);
        await LoadDataAsync();
    }
    [RelayCommand]
    private async Task DeleteActivityAsync(Guid id)
    {
        await _activityFacade.DeleteAsync(id);
        MessengerService.Send(new ActivityDeleteMessage());
    }
    [RelayCommand]
    private async Task GoToActivityEditAsync(Guid id)
    {
        await _navigationService.GoToAsync<ActivityEditViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.ActivityId)] = id }
        );

    }

    public async void Receive(ActivityAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}