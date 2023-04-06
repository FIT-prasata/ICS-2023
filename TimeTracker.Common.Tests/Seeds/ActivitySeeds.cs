using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Enums;
using TimeTracker.Common.Tests.Seeds;

namespace TimeTracker.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static ActivityEntity ActivitySeed1 => new()
    {
        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
        Start = DateTime.Now,
        End = DateTime.Now,
        Description = "Test",
        Type = ActivityType.Work,
        CreatedById = UserSeeds.UserGet.Id,
        ProjectId = ProjectSeeds.ProjectEntity1.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
                ActivitySeed1
            );
    }
}

