using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models
{
    internal record ProjectDetailModel : ModelBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }

        public ObservableCollection<UserDetailModel> CreatedBy { get; init; } = new();
    }
}