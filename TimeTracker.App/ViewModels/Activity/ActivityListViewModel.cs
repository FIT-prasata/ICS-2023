using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Services;
using TimeTracker.App.ViewModels.Activity.Enums;
using TimeTracker.BL.Enums;
using TimeTracker.BL.Facades;
using TimeTracker.BL.Models;

namespace TimeTracker.App.ViewModels.Activity;
public partial class ActivityListViewModel: ViewModelBase
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;


    public List<LazyDateType> TypeFilter { get; set; }
    public IEnumerable<ActivityListModel> FilteredActivities { get; set; } = null!;
    public string? DisplayText { get; set; } = null;
    public CurrentActivityFilter CurrentActivityFilter { get; set; } = CurrentActivityFilter.All;

    public DateTime SpecificDateStart {get; set; } = DateTime.Today;
    public DateTime SpecificDateEnd { get; set; } = DateTime.Today;


    public ActivityListViewModel(
        IActivityFacade activityFacade,
        INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _activityFacade = activityFacade;
        _navigationService = navigationService;

        TypeFilter = Enum.GetValues<LazyDateType>().ToList();
        DisplayText = "All activities";
        CurrentActivityFilter = CurrentActivityFilter.All;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        switch (CurrentActivityFilter)
        {
            case CurrentActivityFilter.All:
                await GetAllActivitiesAsync();
                break;
            case CurrentActivityFilter.Day:
                await GetActivitiesFromLastDayAsync();
                break;
            case CurrentActivityFilter.Week:
                await GetActivitiesFromLastWeekAsync();
                break;  
            case CurrentActivityFilter.Month:
                await GetActivitiesFromLastMonthAsync();
                break;
            case CurrentActivityFilter.Year:
                await GetActivitiesFromLastYearAsync();
                break;
            case CurrentActivityFilter.Custom:
                await GetActivitiesFromSpecificDateAsync();
                break;
        }
    }

    [RelayCommand]
    private async Task GetAllActivitiesAsync()
    {
        FilteredActivities = await _activityFacade.GetAsync();
        DisplayText = "All activities";
        CurrentActivityFilter = CurrentActivityFilter.All;
        await base.LoadDataAsync();
    }
    [RelayCommand]
    private async Task GetActivitiesFromLastDayAsync()
    {
        FilteredActivities = await _activityFacade.GetActivitiesByDateLazyAsync(LazyDateType.Day);
        DisplayText = "Activities of last 24h";
        CurrentActivityFilter = CurrentActivityFilter.Day;
        await base.LoadDataAsync();
    }

    [RelayCommand]
    private async Task GetActivitiesFromLastWeekAsync()
    {
        FilteredActivities = await _activityFacade.GetActivitiesByDateLazyAsync(LazyDateType.Week);
        DisplayText = "Activities of last week";
        CurrentActivityFilter = CurrentActivityFilter.Week;
        await base.LoadDataAsync();
    }

    [RelayCommand]
    private async Task GetActivitiesFromLastMonthAsync()
    {
        FilteredActivities = await _activityFacade.GetActivitiesByDateLazyAsync(LazyDateType.Month);
        DisplayText = "Activities of last month";
        CurrentActivityFilter = CurrentActivityFilter.Month;
        await base.LoadDataAsync();
    }

    [RelayCommand]
    private async Task GetActivitiesFromLastYearAsync()
    {
        FilteredActivities = await _activityFacade.GetActivitiesByDateLazyAsync(LazyDateType.Year);
        DisplayText = "Activities of last year";
        CurrentActivityFilter = CurrentActivityFilter.Year;
        await base.LoadDataAsync();
    }

    [RelayCommand]
    private async Task GetActivitiesFromSpecificDateAsync()
    {
        FilteredActivities = await _activityFacade.GetActivitiesByDateAsync(SpecificDateStart, SpecificDateEnd + new TimeSpan(23,59,59));
        DisplayText = $"Activities from {SpecificDateStart.ToShortDateString()} to {SpecificDateEnd.ToShortDateString()}";
        CurrentActivityFilter = CurrentActivityFilter.Custom;
        await base.LoadDataAsync();
    }
    [RelayCommand]
    private async Task DeleteActivityAsync(Guid id)
    {
        await _activityFacade.DeleteAsync(id);
        await LoadDataAsync();
    }
    [RelayCommand]
    private async Task GoToActivityEditAsync(Guid id)
    {
        await _navigationService.GoToAsync<ActivityEditViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityEditViewModel.ActivityId)] = id }
        );
    }
}
