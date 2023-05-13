using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models
{
    public record UserListModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public string? ImgUri { get; set; }


        public static UserListModel Empty => new()
        {
            Id = Guid.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
        };

    }
}