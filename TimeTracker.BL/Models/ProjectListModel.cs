using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models
{
    public record ProjectListModel: ModelBase
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public static ProjectListModel Empty => new()
        {
            Id = Guid.Empty,
            Name = string.Empty,
            Description = string.Empty,
        };
    }
}
