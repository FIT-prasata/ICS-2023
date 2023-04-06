using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests.Seeds;

public static class ProjectSeeds
{
    public static ProjectEntity ProjectGet => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0001-000000000001"),
        Name = "Test Project 1",
        Description = "Test Project Description 1",
        CreatedById = UserSeeds.UserEntity1.Id,
    };

    public static ProjectEntity ProjectUpdate => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0001-000000000002"),
        Name = "Test Project 2",
        Description = "Test Project Description 2",
        CreatedById = UserSeeds.UserEntity1.Id,
    };

    public static ProjectEntity ProjectDelete => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0001-000000000003"),
        Name = "Test Project 3",
        Description = "Test Project Description 3",
        CreatedById = UserSeeds.UserEntity1.Id,
    };

    public static ProjectEntity ProjectEntity1 => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0001-000000000004"),
        Name = "Test Project 4",
        Description = "Test Project Description 4",
        CreatedById = UserSeeds.UserEntity1.Id,
    };


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>().HasData(
            ProjectGet,
            ProjectUpdate,
            ProjectDelete,
            ProjectEntity1
            );
    }
}

