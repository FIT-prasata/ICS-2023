using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Facades.Interfaces;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class ProjectFacade: FacadeBase<ProjectEntity, ProjectDetailModel, ProjectListModel, ProjectEntityMapper>

    {
        public ProjectFacade(ProjectModelMapper mapper, IUnitOfWorkFactory uow) : base(mapper, uow)
        {
        }
        protected override List<string> IncludesNavigationPathDetail => new()
        {
            $"{nameof(ProjectEntity.Users)}.{nameof(ProjectUserEntity.UserEntity)}",
            $"{nameof(ProjectEntity.Activities)}",
        };
    }
}
