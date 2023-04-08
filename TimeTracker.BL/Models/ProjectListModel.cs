using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models
{
    internal record ProjectListModel: ModelBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
    }
}
