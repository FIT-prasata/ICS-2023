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
    public class ProjectFacade: FacadeDetailMixin<ProjectEntity, ProjectDetailModel, ProjectEntityMapper>, IFacadeListMixin<ProjectEntity,ProjectListModel>

    {
        public ProjectFacade(IUnitOfWorkFactory iow, ProjectModelMapper mapper) : base(iow, mapper)
        {
        }
        protected override List<string> IncludesNavigationPathDetail => new()
        {
            $"{nameof(ProjectEntity.Users)}.{nameof(ProjectUserEntity.UserEntity)}",
            $"{nameof(ProjectEntity.Activities)}",
        };
    }
}
