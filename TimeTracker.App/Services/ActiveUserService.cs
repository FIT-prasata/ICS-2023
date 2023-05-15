using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.App.Services.Interfaces;

namespace TimeTracker.App.Services;
    public class ActiveUserService: IActiveUserService
    {
        private Guid Id { get; set; } = Guid.Empty;
        public void SetId(Guid id) => Id = id;
        public Guid GetId() => Id;
        public Boolean IsAuthenticated() => Id != Guid.Empty;
    }
