using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.App.ViewModels.User;

namespace TimeTracker.App.Shells;

public partial class AppShell: IRecipient<UserAuthenticatedMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;
    public Boolean IsAuthenticated { get; set; } = false;

    public AppShell(INavigationService navigationService, IActiveUserService activeUserService)
    { 
        _navigationService = navigationService;
        _activeUserService = activeUserService;

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

    public void Receive(UserAuthenticatedMessage message)
    {
        IsAuthenticated = _activeUserService.IsAuthenticated();
    }
}