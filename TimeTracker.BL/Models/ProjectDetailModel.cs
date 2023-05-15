﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models
{
    public record ProjectDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid CreatedById { get; set; } 
        public ObservableCollection<ActivityListModel>? Activities { get; init; } = new();
        public ObservableCollection<UserListModel>? Users { get; init; } = new();

        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Name = string.Empty,
            Description = string.Empty,
            CreatedById = Guid.Empty,
        };

        public ProjectDetailModel WithoutRelatedProperties()
        {
            return this with { Activities = null, Users = null};
        }
    }
}