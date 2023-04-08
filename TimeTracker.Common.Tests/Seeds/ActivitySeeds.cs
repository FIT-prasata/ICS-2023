using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Enums;
using TimeTracker.Common.Tests.Seeds;

namespace TimeTracker.Common.Tests.Seeds;

public static class ActivitySeeds

{

 
    public static ActivityEntity ActivityGet => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
        Start = DateTime.Parse("2023-01-01 00:00:00"),
        End = DateTime.Parse("2023-01-01 00:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };

    public static ActivityEntity ActivityUpdate => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
        Start = DateTime.Parse("2023-01-01 00:00:00"),
        End = DateTime.Parse("2023-01-01 00:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };
    public static ActivityEntity ActivityDelete => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
        Start = DateTime.Parse("2023-01-01 00:00:00"),
        End = DateTime.Parse("2023-01-01 00:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
                ActivityGet,
                ActivityUpdate,
                ActivityDelete
            );
    }
}

