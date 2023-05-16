using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;
    private readonly IAlertService _alertService;

    public AppShell(INavigationService navigationService, IActiveUserService activeUserService, IAlertService alertService)
    {
        _navigationService = navigationService;
        _activeUserService = activeUserService;
        _alertService = alertService;

        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToProjectsAsync()
    {
        if (!_activeUserService.IsAuthenticated())
        {
            await _alertService.DisplayAsync("Error", "Please select user first");
            return;
        }
        await _navigationService.GoToAsync<ProjectListViewModel>();
    }


    [RelayCommand]
    private async Task GoToActivitiesAsync()
    {
        if (!_activeUserService.IsAuthenticated())
        {
            await _alertService.DisplayAsync("Error", "Please select user first");
            return;
        }
        await _navigationService.GoToAsync<ActivityListViewModel>();
    }

    [RelayCommand]
    private async Task GoToUserAsync()
    {
        if (!_activeUserService.IsAuthenticated())
        {
            await _alertService.DisplayAsync("Error", "Please select user first");
            return;
        }
        await _navigationService.GoToAsync<UserDetailViewModel>();
    }

}