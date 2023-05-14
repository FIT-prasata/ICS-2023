using Microsoft.Extensions.Configuration;
using TimeTracker.App.Options;
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
            DALOptions dalOptions = new();
            configuration.GetSection("TimeTracker:DAL").Bind(dalOptions);

            services.AddSingleton<DALOptions>(dalOptions);

            if (dalOptions.LocalDb is null && dalOptions.Sqlite is null)
            {
                throw new InvalidOperationException("No persistence provider configured");
            }

            if (dalOptions.LocalDb?.Enabled == false && dalOptions.Sqlite?.Enabled == false)
            {
                throw new InvalidOperationException("No persistence provider enabled");
            }

            if ((dalOptions.LocalDb?.Enabled == true) && (dalOptions.Sqlite?.Enabled == true))
            {
                throw new InvalidOperationException("Both persistence providers enabled");
            }

            if (dalOptions.LocalDb?.Enabled == true)
            {
                services.AddSingleton<IDbContextFactory<TimeTrackerDbContext>>(provider => new SqlServerDbContextFactory(dalOptions.LocalDb.ConnectionString));
                services.AddSingleton<IDbMigrator, NoneDbMigrator>();
            }

            if (dalOptions.Sqlite?.Enabled == true)
            {
                if (dalOptions.Sqlite.DatabaseName is null)
                {
                    throw new InvalidOperationException($"{nameof(dalOptions.Sqlite.DatabaseName)} is not set");

                }
                string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName!);
                services.AddSingleton<IDbContextFactory<TimeTrackerDbContext>>(provider => new DbContextSqLiteFactory(databaseFilePath, dalOptions?.Sqlite?.SeedDemoData ?? false));
                services.AddSingleton<IDbMigrator, SqliteDbMigrator>();
            }

            services.AddSingleton<ProjectEntityMapper>();
            services.AddSingleton<UserEntityMapper>();
            services.AddSingleton<ActivityEntityMapper>();
            services.AddSingleton<ProjectUserEntityMapper>();

            return services;
        }
    }

}
