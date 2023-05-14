using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Services.Interfaces;
    public interface IActiveUserService
    {
        Guid GetId() => Guid.Parse("86aae9b6-46ff-4ded-8562-dc5dcd06c39b");
    }
