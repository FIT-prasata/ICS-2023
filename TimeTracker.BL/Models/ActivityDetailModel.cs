using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Models
{
    internal class ActivityDetailModel
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Description { get; set; }
        public ActivityType Type { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? AssignedId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
