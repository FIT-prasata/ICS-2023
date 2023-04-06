using TimeTracker.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TimeTracker.Common.Tests.Seeds;

public static class UserSeeds
{
    public static UserEntity UserGet => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0000-000000000001"),
        FirstName = "John",
        LastName = "Doe",
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
    };
    public static UserEntity UserUpdate => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0000-000000000002"),
        FirstName = "Jane",
        LastName = "Doe",
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
    };
    public static UserEntity UserDelete => new()
    {
        Id = Guid.Parse(input: "00000000-0000-0000-0000-000000000003"),
        FirstName = "John",
        LastName = "Smith",
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserGet,
            UserUpdate,
            UserDelete
            );
    }
}

