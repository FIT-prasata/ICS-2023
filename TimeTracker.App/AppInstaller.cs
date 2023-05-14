using CommunityToolkit.Mvvm.Messaging;
using TimeTracker.App.Services;
using TimeTracker.App.Services.Interfaces;
using TimeTracker.App.Shells;
using TimeTracker.App.ViewModels;
using TimeTracker.App.Views;

namespace TimeTracker.App;

public static class AppInstaller
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();

        services.AddSingleton<IMessenger>(_ => StrongReferenceMessenger.Default);
        services.AddSingleton<IMessengerService, MessengerService>();

        services.AddSingleton<IAlertService, AlertService>();
        services.AddSingleton<IActiveUserService, ActiveUserService>();

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<ContentBasePage>())
            .AsSelf()
            .WithTransientLifetime());

        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<IViewModel>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());

        services.AddTransient<INavigationService, NavigationService>();

        return services;
    }
}


