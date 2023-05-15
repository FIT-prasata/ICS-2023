using TimeTracker.App.Models;
using TimeTracker.App.ViewModels;
using TimeTracker.App.ViewModels.Activity;
using TimeTracker.App.ViewModels.Project;
using TimeTracker.App.ViewModels.User;
using TimeTracker.App.Views.Activity;
using TimeTracker.App.Views.Project;
using TimeTracker.App.Views.User;

namespace TimeTracker.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        new("//projects", typeof(ProjectListView), typeof(ProjectListViewModel)),
        new("//projects/detail", typeof(ProjectDetailView), typeof(ProjectDetailViewModel)),
        new("//activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//user", typeof(UserDetailView), typeof(UserDetailViewModel))
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel 
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}