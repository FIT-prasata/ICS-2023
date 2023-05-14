﻿using Microsoft.EntityFrameworkCore;
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
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
    };
    public static UserEntity UserUpdate => new()
    {
        Id = Guid.Parse(input: "8b34c677-85d6-4659-9d1b-3ca3e2aef80e"),
        FirstName = "Jane",
        LastName = "Doe",
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
    };
    public static UserEntity UserDelete => new()
    {
        Id = Guid.Parse(input: "1a8ee247-0519-4be9-9d0a-ee99fdece4f4"),
        FirstName = "John",
        LastName = "Smith",
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
    };

    public static UserEntity UserEntity1 => new()
    {
        Id = Guid.Parse(input: "c02a8a4f-f4d1-44db-9f60-f28f97e2b010"),
        FirstName = "Igor",
        LastName = "Rogi",
        ImgUri = "https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png"
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
