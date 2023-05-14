using Microsoft.Extensions.Configuration;
using TimeTracker.DAL.Factories;
using TimeTracker.DAL;
using TimeTracker.DAL.Mappers;
using Microsoft.EntityFrameworkCore;


namespace TimeTracker.App
{
    public static class DALInstaller
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, "TimeTracker.sqlite");
            services.AddSingleton<IDbContextFactory<TimeTrackerDbContext>>(provider => new DbContextSqLiteFactory(databaseFilePath, false));
            services.AddSingleton<IDbMigrator, SqliteDbMigrator>();

            services.AddSingleton<ProjectEntityMapper>();
            services.AddSingleton<UserEntityMapper>();
            services.AddSingleton<ActivityEntityMapper>();
            services.AddSingleton<ProjectUserEntityMapper>();

            return services;
        }
    }

}
