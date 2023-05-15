using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Enums;

namespace TimeTracker.DAL.Seeds;

public static class ActivitySeeds

{
    public static readonly int NumActivities = 8;
    public static readonly int NumActivitiesInJanuary2020 = 4;
 
    public static ActivityEntity ActivityGet => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
        Start = DateTime.Parse("2020-01-22 00:00:00"),
        End = DateTime.Parse("2020-01-22 01:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectGet.Id,
        AssignedId = UserSeeds.UserGet.Id,
    };

    public static ActivityEntity ActivityUpdate => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
        Start = DateTime.Parse("2020-01-02 04:00:00"),
        End = DateTime.Parse("2020-01-02 12:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        AssignedId = UserSeeds.UserEntity1.Id

    };
    public static ActivityEntity ActivityDelete => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
        Start = DateTime.Parse("2020-01-11 01:00:00"),
        End = DateTime.Parse("2020-01-11 03:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };

    public static ActivityEntity ActivityFromToday => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
        Start = DateTime.Now,
        End = DateTime.Now.AddHours(2),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };
    
    public static ActivityEntity ActivityAlmostWeekAgo => new ()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
        Start = DateTime.Now.AddHours(-2).AddDays(-6),
        End = DateTime.Now.AddDays(-6),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };

    public static ActivityEntity ActivityAlmostMonthAgo => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
        Start = DateTime.Now.AddHours(-2).AddDays(1).AddMonths(-1),
        End = DateTime.Now.AddDays(1).AddMonths(-1),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };

    public static ActivityEntity ActivityAlmostYearAgo => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000007"),
        Start = DateTime.Now.AddHours(-2).AddDays(1).AddYears(-1),
        End = DateTime.Now.AddDays(1).AddYears(-1),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };

    public static ActivityEntity ActivityAssignedToUser => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000008"),
        Start = DateTime.Parse("2020-01-22 03:00:00"),
        End = DateTime.Parse("2020-01-22 12:00:00"),
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserEntity1.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id,
        AssignedId = UserSeeds.UserEntity1.Id
    };



    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
                ActivityGet,
                ActivityUpdate,
                ActivityDelete,
                ActivityFromToday,
                ActivityAlmostWeekAgo,
                ActivityAlmostMonthAgo,
                ActivityAlmostYearAgo,
                ActivityAssignedToUser
            );
    }
}

