using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.BL.Models
{
    public record UserDetailModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string? ImgUri { get; set; }

        public static UserDetailModel Empty => new()
        {
            Id = Guid.Empty,
            FirstName = string.Empty,
            LastName = string.Empty,
            ImgUri = string.Empty
        };
 
    }
}
