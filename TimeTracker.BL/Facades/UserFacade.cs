using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTracker.BL.Mappers;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;
using TimeTracker.DAL.Mappers;
using TimeTracker.DAL.UnitOfWork;

namespace TimeTracker.BL.Facades
{
    public class UserFacade: FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
    {
        public UserFacade(IUserModelMapper mapper, IUnitOfWorkFactory uow) : base(mapper, uow)
        {
        }

        public async Task<IEnumerable<UserListModel>> GetUsersNotInProjectAsync(Guid projectId)
        {
            await using IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<UserEntity> query = uow.GetRepository<UserEntity, UserEntityMapper>().Get();
            IQueryable<ProjectUserEntity> projectUsers = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>().Get();
            IEnumerable<Guid> userIds = projectUsers.Where(pu => pu.ProjectEntityId == projectId).Select(pu => pu.UserEntityId);
            return Mapper.MapToListModel(await (query.Where(u => !userIds.Contains(u.Id))).ToListAsync());
        }
    }
}
