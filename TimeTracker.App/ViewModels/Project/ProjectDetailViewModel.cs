using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Enums;

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

    public UserDetailModel NewUser { get; set; } = UserDetailModel.Empty;

    public ActivityDetailModel NewActivity {get; set; } = ActivityDetailModel.Empty;

    public DateTime DateStart { get; set; } = DateTime.Now;
    public DateTime DateEnd { get; set; } = DateTime.Now;
    public TimeSpan TimeStart { get; set; } = new TimeSpan(0,0,0);
    public TimeSpan TimeEnd { get; set; } = new TimeSpan(0,0,0);
    public List<ActivityType> ActivityTypes { get; set; }
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

        ActivityTypes = Enum.GetValues<ActivityType>().Where(a => a != ActivityType.Empty).ToList();

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

    [RelayCommand]
    private async Task AddNewUserAsync()
    {
        if (NewUser.FirstName is null || NewUser.LastName is null )
        {
            await _alertService.DisplayAsync("Error", "First and last names are required");
            return;
        }
        NewUser.Id = Guid.NewGuid();
        await _userFacade.SaveAsync(NewUser);
        await _projectFacade.AddUserToProjectAsync(ProjectId, NewUser.Id);
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task AddActivityAsync()
    {
        if (NewActivity.Type == ActivityType.Empty)
        {
            await _alertService.DisplayAsync("Error", "Select type");
            return;
        }
        NewActivity.Id = Guid.NewGuid();
        NewActivity.ProjectId = ProjectId;
        NewActivity.Start = DateStart + TimeStart;
        NewActivity.End = DateEnd + TimeEnd;
        UserDetailModel user = (await _userFacade.GetAsync(_activeUserService.GetId()))!;
        NewActivity.CreatedBy = user;
        NewActivity.Assigned = user;
        try
        {
            await _activityFacade.SaveAsync(NewActivity);
        }
        catch (SecurityTokenException e)
        {
            await _alertService.DisplayAsync("Error", e.Message);
        }
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task DeleteActivityAsync(Guid id)
    {
        await _activityFacade.DeleteAsync(id);
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToProjectEditAsync()
    {
        await _navigationService.GoToAsync<ProjectEditViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectEditViewModel.ProjectId)] = ProjectId }
            );
    }
    [RelayCommand]
    private async Task GoToActivityEditAsync(Guid id)
    {
        await _navigationService.GoToAsync<ActivityEditViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.ActivityId)] = id }
        );
    }

}
