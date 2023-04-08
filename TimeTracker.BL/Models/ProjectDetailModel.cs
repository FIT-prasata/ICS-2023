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

        public Guid CreatedById { get; init; } = new();
        public ObservableCollection<ActivityListModel>? Activities { get; init; } = new();
        public ObservableCollection<ProjectUserListModel>? Users { get; init; } = new();

        public static ProjectDetailModel Empty => new()
        {
            Id = Guid.Empty,
            Name = string.Empty,
            Description = string.Empty,
            CreatedById = Guid.Empty,
        };
    }
}