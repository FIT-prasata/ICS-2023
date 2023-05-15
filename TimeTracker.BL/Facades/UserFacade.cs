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

        public async Task<IEnumerable<UserListModel>> GetByProjectAsync(Guid projectId)
        {
            IUnitOfWork uow = UnitOfWorkFactory.Create();
            IQueryable<ProjectUserEntity> query = uow.GetRepository<ProjectUserEntity, ProjectUserEntityMapper>().Get();
            query = query.Include(nameof(ProjectUserEntity.UserEntity));
            List<ProjectUserEntity> entities = await query.Where(pu => pu.ProjectEntityId == projectId).ToListAsync();
            if (entities.Count == 0)
            {
                return new List<UserListModel>();
            }

            List<UserEntity> users = entities.Select(pu => pu.UserEntity).ToList();
            return Mapper.MapToListModel(users);
        }

    }
}
