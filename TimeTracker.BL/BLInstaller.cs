using TimeTracker.BL.Facades;
using TimeTracker.BL.Mappers;
using TimeTracker.DAL.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using TimeTracker.DAL.Mappers;

namespace TimeTracker.BL;

public static class BLInstaller
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        services.AddSingleton<IProjectFacade, ProjectFacade>();
        services.AddSingleton<IUserFacade, UserFacade>();
        services.AddSingleton<IActivityFacade, ActivityFacade>();

        services.AddSingleton<IProjectModelMapper, ProjectModelMapper>();
        services.AddSingleton<IUserModelMapper, UserModelMapper>();
        services.AddSingleton<IActivityModelMapper, ActivityModelMapper>();
        //services.Scan(selector => selector
        //    .FromAssemblyOf<BusinessLogic>()
        //    .AddClasses(filter => filter.AssignableTo(typeof(IModelMapper<,,>)))
        //    .AsMatchingInterface()
        //    .WithSingletonLifetime());

        //services.Scan(selector => selector
        //    .FromAssemblyOf<BusinessLogic>()
        //    .AddClasses(filter => filter.AssignableTo(typeof(IFacade<,,>)))
        //    .AsMatchingInterface()
        //    .WithSingletonLifetime());

        

        return services;
    }
}