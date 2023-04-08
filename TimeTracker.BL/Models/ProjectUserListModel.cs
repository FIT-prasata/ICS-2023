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
        public ObservableCollection<UserDetailModel> User { get; init; } = new();
        public ObservableCollection<ProjectDetailModel> Project { get; init; } = new();
    }

}