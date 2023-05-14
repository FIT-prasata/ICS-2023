using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Services;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Shells;

public partial class AppShell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }

    [RelayCommand]
    private async Task GoToProjectsAsync()
    => await _navigationService.GoToAsync<ProjectListViewModel>();

    [RelayCommand]
    private async Task GoToActivitiesAsync()
    => await _navigationService.GoToAsync<ActivityListViewModel>();

    [RelayCommand]
    private async Task GoToUserAsync()
    => await _navigationService.GoToAsync<UserDetailViewModel>();
}