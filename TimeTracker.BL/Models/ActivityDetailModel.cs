using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Models
{
    public record ActivityDetailModel: ModelBase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Description { get; set; }
        public ActivityType Type { get; set; }

        public UserDetailModel CreatedBy { get; set; }
        public UserDetailModel Assigned { get; set; } 
        public Guid ProjectId { get; set; }


        public static ActivityDetailModel Empty => new()
        {
            Id = Guid.NewGuid(),
            Start = DateTime.Now,
            End = DateTime.Now,
            Description = string.Empty,
            Type = ActivityType.Empty,
            CreatedBy = UserDetailModel.Empty,
            Assigned = UserDetailModel.Empty,
            ProjectId = Guid.Empty
        };
    }

}
