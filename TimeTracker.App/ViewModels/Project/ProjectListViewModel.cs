using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Messages;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Project;
public partial class ProjectListViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;
    private readonly IActiveUserService _activeUserService;

    public IEnumerable<ProjectListModel> Projects { get; set; } = null!;
    public ProjectDetailModel NewProject { get; set; } = ProjectDetailModel.Empty;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IActiveUserService activeUserService,
        IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;
        _activeUserService = activeUserService;

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
            // TODO error toast
            return;
        }
        NewProject.Id = Guid.NewGuid();
        NewProject.CreatedById = _activeUserService.GetId();
        await _projectFacade.SaveAsync(NewProject);
        NewProject = ProjectDetailModel.Empty;
        await LoadDataAsync();
    }

}
