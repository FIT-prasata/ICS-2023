using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades;
    public interface IUserFacade: IFacade<UserEntity, UserListModel, UserDetailModel>
    {
        Task<IEnumerable<UserListModel>> GetUsersNotInProjectAsync(Guid projectId);
    }
