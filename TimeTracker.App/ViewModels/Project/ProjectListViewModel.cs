﻿using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Project;
public partial class ProjectListViewModel : ViewModelBase, IRecipient<ProjectEditMessage>, IRecipient<ProjectDeleteMessage>
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;
    private readonly IAlertService _alertService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;
    public ProjectDetailModel NewProject { get; set; } = ProjectDetailModel.Empty;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IActiveUserService activeUserService,
        IAlertService alertService,
        IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _activeUserService = activeUserService;
        _alertService = alertService;


    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Projects = await _projectFacade.GetAsync();
    }

    [RelayCommand]
    private async Task AddProjectAsync()
    {
        if (NewProject.Name == string.Empty || NewProject.Description == string.Empty)
        {
            await _alertService.DisplayAsync("Error", "Please fill out both fields");
            return;
        }
        NewProject.CreatedById = _activeUserService.GetId();
        NewProject.Id = Guid.NewGuid();
        await _projectFacade.SaveAsync(NewProject);
        NewProject = ProjectDetailModel.Empty;
        await LoadDataAsync();
    }

    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await _navigationService.GoToAsync<ProjectDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ProjectDetailViewModel.ProjectId)] = id });

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
