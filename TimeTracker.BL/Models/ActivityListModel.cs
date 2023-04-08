using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Enums;

namespace TimeTracker.BL.Models
{
    internal record ActivityListModel : ModelBase
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public ActivityType Type { get; set; }
        public ObservableCollection<UserDetailModel> Assigned { get; init; } = new();
        public ObservableCollection<ProjectDetailModel> Project { get; init; } = new();


    }

}