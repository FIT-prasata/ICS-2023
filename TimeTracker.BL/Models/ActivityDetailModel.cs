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
        public string Description { get; set; }
        public ActivityType Type { get; set; }

        public ObservableCollection<UserDetailModel> CreatedBy { get; init; } = new();
        public ObservableCollection<UserDetailModel> Assigned { get; init; } = new();
        public ObservableCollection<ProjectDetailModel> Project { get; init; } = new();

    }

}
