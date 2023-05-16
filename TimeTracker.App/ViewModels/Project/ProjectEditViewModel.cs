using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using TimeTracker.App.Messages;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Project;

[QueryProperty(nameof(ProjectId), nameof(ProjectId))]
public partial class ProjectEditViewModel: ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;
    private readonly IAlertService _alertService;

    public Guid ProjectId { get; set; }

    public ProjectDetailModel? Project { get; set; }

    public ProjectEditViewModel(IProjectFacade projectFacade, INavigationService navigationService,
        IActiveUserService activeUserService, IAlertService alertService, IMessengerService messengerService) : base(
        messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _activeUserService = activeUserService;
        _alertService = alertService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        if (ProjectId == Guid.Empty)
        {
            Project = ProjectDetailModel.Empty;
        }
        else
        {
            Project = await _projectFacade.GetAsync(ProjectId);
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Project.Name == string.Empty)
        {
            await _alertService.DisplayAsync("Error", "Please fill out the name field");
            return;
        }
        await _projectFacade.SaveAsync(Project);
        MessengerService.Send(new ProjectEditMessage {ProjectId = ProjectId});
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        await _projectFacade.DeleteAsync(ProjectId);
        MessengerService.Send(new ProjectDeleteMessage());
        await _navigationService.GoToAsync<ProjectListViewModel>();
    }

}