using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades.Interfaces;
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
