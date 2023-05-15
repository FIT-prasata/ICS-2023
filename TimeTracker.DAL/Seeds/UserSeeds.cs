using Microsoft.EntityFrameworkCore;
using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Seeds;

public static class UserSeeds
{

    public static readonly int NumUsers = 4;
    public static UserEntity UserGet => new()
    {
        Id = Guid.Parse(input: "86aae9b6-46ff-4ded-8562-dc5dcd06c39b"),
        FirstName = "John",
        LastName = "Doe",
        ImgUri = "https://i.imgur.com/9wjVTB0.png"
    };
    public static UserEntity UserUpdate => new()
    {
        Id = Guid.Parse(input: "8b34c677-85d6-4659-9d1b-3ca3e2aef80e"),
        FirstName = "Jane",
        LastName = "Doe",
        ImgUri = "https://www.gannett-cdn.com/-mm-/7906a999b31790bc0f9310fb64675301c3b480fd/c=130-0-6663-4912/local/-/media/2017/10/24/USATODAY/USATODAY/636444395541853578-GettyImages-586714878.jpg?width=2560"
    };

    public static UserEntity UserDelete => new()
    {
        Id = Guid.Parse(input: "1a8ee247-0519-4be9-9d0a-ee99fdece4f4"),
        FirstName = "John",
        LastName = "Smith",
        ImgUri = "https://i.imgur.com/8mrDrMd.jpg"
    };

    public static UserEntity UserEntity1 => new()
    {
        Id = Guid.Parse(input: "c02a8a4f-f4d1-44db-9f60-f28f97e2b010"),
        FirstName = "Růža",
        LastName = "ImaEnjoyer",
        ImgUri = "https://i.imgur.com/yC0HcZT.png"
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            UserGet,
            UserUpdate,
            UserDelete,
            UserEntity1
            );
    }
}

