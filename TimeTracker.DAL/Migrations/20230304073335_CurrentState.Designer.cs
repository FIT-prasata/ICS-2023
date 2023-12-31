﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTracker.DAL;

#nullable disable

namespace TimeTracker.DAL.Migrations
{
    [DbContext(typeof(TimeTrackerDbContext))]
    [Migration("20230304073335_CurrentState")]
    partial class CurrentState
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("TimeTracker.DAL.Entities.ActivityEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("AssignedId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AssignedId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ProjectId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.ProjectEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.ProjectUserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProjectEntityId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserEntityId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProjectEntityId");

                    b.HasIndex("UserEntityId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImgUri")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.ActivityEntity", b =>
                {
                    b.HasOne("TimeTracker.DAL.Entities.UserEntity", "Assigned")
                        .WithMany("Activities")
                        .HasForeignKey("AssignedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TimeTracker.DAL.Entities.UserEntity", "CreatedBy")
                        .WithMany("AuthoredActivities")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTracker.DAL.Entities.ProjectEntity", "Project")
                        .WithMany("Activities")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assigned");

                    b.Navigation("CreatedBy");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.ProjectEntity", b =>
                {
                    b.HasOne("TimeTracker.DAL.Entities.UserEntity", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.ProjectUserEntity", b =>
                {
                    b.HasOne("TimeTracker.DAL.Entities.ProjectEntity", "ProjectEntity")
                        .WithMany("Users")
                        .HasForeignKey("ProjectEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTracker.DAL.Entities.UserEntity", "UserEntity")
                        .WithMany("Projects")
                        .HasForeignKey("UserEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProjectEntity");

                    b.Navigation("UserEntity");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.ProjectEntity", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("TimeTracker.DAL.Entities.UserEntity", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("AuthoredActivities");

                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
