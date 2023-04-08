﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Models
{
    public record ActivityListModel : ModelBase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ActivityType Type { get; set; }
        public Guid? AssignedId { get; set; }
        public Guid ProjectId { get; set; } 

        public static ActivityListModel Empty => new()
        {
            Id = Guid.Empty,
            Start = DateTime.Now,
            End = DateTime.Now,
            Type = ActivityType.Empty,
            AssignedId = Guid.Empty,
            ProjectId = Guid.Empty,
        };

    }

}