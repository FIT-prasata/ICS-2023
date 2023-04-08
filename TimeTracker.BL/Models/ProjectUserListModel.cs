using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Models
{
    public record ProjectUserListModel : ModelBase
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        public static ProjectUserListModel Empty => new()
        {
            Id = Guid.Empty,
            UserId = Guid.Empty,
            ProjectId = Guid.Empty,
        };
    }

}