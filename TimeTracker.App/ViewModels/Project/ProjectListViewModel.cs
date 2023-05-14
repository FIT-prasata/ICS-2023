using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Project;
public partial class ProjectListViewModel : ViewModelBase
{
    private readonly IProjectFacade _projectFacade;
    private readonly INavigationService _navigationService;

    public IEnumerable<ProjectListModel> Activities { get; set; } = null!;

    public ProjectListViewModel(
        IProjectFacade projectFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _projectFacade = projectFacade;
        _navigationService = navigationService;

    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Activities = await _projectFacade.GetAsync();
    }
}
