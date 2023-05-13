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
using TimeTracker.DAL.Repositories;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class ProjectFacade : FacadeBase<ProjectEntity, ProjectDetailModel, ProjectListModel, ProjectEntityMapper>,
        IProjectFacade

    {
        public ProjectFacade(ProjectModelMapper mapper, IUnitOfWorkFactory uow) : base(mapper, uow)
        {
        }

        protected override List<string> IncludesNavigationPathDetail => new()
        {
            $"{nameof(ProjectEntity.Users)}.{nameof(ProjectUserEntity.UserEntity)}",
            $"{nameof(ProjectEntity.Activities)}",
        };

        public async Task AddUserToProjectAsync(Guid projectId, Guid userId)
        {
            IUnitOfWork uow = UnitOfWorkFactory.Create();
            IRepository<ProjectUserEntity> repository = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>();
            ProjectUserEntity projectUser = new()
            {
                Id = Guid.NewGuid(),
                ProjectEntityId = projectId,
                UserEntityId = userId
            };
            await repository.InsertAsync(projectUser);
            await uow.CommitAsync();

        }

        public async Task RemoveUserFromProjectAsync(Guid projectId, Guid userId)
        {
            IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<ProjectUserEntity> query = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>().Get();
            ProjectUserEntity? projectUser =
                query.FirstOrDefault(pu => pu.ProjectEntityId == projectId && pu.UserEntityId == userId);
            if (projectUser is null)
            {
                throw new InvalidOperationException("User is not assigned to this project.");
            }

            uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>().Delete(projectUser.Id);
            await uow.CommitAsync().ConfigureAwait(false);
        }
    }
}
