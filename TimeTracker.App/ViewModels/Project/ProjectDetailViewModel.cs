using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Project;
[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
public partial class ProjectDetailViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;
    private readonly IAlertService _alertService;

    public Guid ProjectId { get; set; }

    public ProjectDetailModel? Project { get; set; }
    public Boolean IsUserAssigned { get; set; } = false;

    public Boolean IsNotUserAssigned => !IsUserAssigned;

    public IEnumerable<UserListModel> Users { get; set; } = new List<UserListModel>() ;
    public UserListModel? SelectedUser { get; set; }

    public ProjectDetailViewModel(IProjectFacade projectFacade, IUserFacade userFacade,
        IActivityFacade activityFacade, INavigationService navigationService, IActiveUserService activeUserService, IAlertService alertService, IMessengerService messengerService)
    : base(messengerService)
    {
        _projectFacade = projectFacade;
        _userFacade = userFacade;
        _activityFacade = activityFacade;
        _navigationService = navigationService;
        _activeUserService = activeUserService;
        _alertService = alertService;

    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Project = await _projectFacade.GetAsync(ProjectId);
        IsUserAssigned = await _projectFacade.IsUserInProjectAsync(ProjectId, _activeUserService.GetId());
        Users = (await _userFacade.GetUsersNotInProjectAsync(ProjectId)).ToList();
    }

    [RelayCommand]
    private async Task AddLoggedUserToProjectAsync()
    {
        await _projectFacade.AddUserToProjectAsync(ProjectId, _activeUserService.GetId());
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task RemoveLoggedUserFromProjectAsync()
    {
        await _projectFacade.RemoveUserFromProjectAsync(ProjectId, _activeUserService.GetId());
        await LoadDataAsync();
    }
    [RelayCommand]
    private async Task RemoveSpecificUserFromProjectAsync(Guid id)
    {
        await _projectFacade.RemoveUserFromProjectAsync(ProjectId, id);
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task AddSelectedUserAsync()
    {
        if (SelectedUser is not null)
        {
            await _projectFacade.AddUserToProjectAsync(ProjectId, SelectedUser.Id);
            await LoadDataAsync();
            return;
        }
        await _alertService.DisplayAsync("Warning", "You need to select user first");
    }
}
