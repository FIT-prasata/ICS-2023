using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Project;
    [QueryProperty(nameof(ProjectId), nameof(ProjectId))]
    public partial class ProjectDetailViewModel: ViewModelBase
    {
        private readonly IProjectFacade _projectFacade;
        private readonly IUserFacade _userFacade;
        private readonly IActivityFacade _activityFacade;
        private readonly INavigationService _navigationService;

        public Guid ProjectId { get; set; }

        public ProjectDetailModel? Project { get; set; }

        public ProjectDetailViewModel(IProjectFacade projectFacade, IUserFacade userFacade,
            IActivityFacade activityFacade, INavigationService navigationService, IMessengerService messengerService)
        : base(messengerService)
        {
            _projectFacade = projectFacade;
            _userFacade = userFacade;
            _activityFacade = activityFacade;
            _navigationService = navigationService;
        }

        protected override async Task LoadDataAsync()
        {
            await base.LoadDataAsync();

            Project = await _projectFacade.GetAsync(ProjectId);

        }
    }
