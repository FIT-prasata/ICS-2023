using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTracker.App.Services.Interfaces;
    public interface IActiveUserService
    {
        void SetId(Guid id);
        Guid GetId();
        Boolean IsAuthenticated();
    }
