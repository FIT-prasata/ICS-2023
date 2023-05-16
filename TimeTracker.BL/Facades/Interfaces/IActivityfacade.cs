using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Enums;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades
{
    public interface IActivityFacade : IFacade<ActivityEntity, ActivityListModel, ActivityDetailModel>
    {
        Task<IEnumerable<ActivityListModel>> GetActivitiesByDateAsync(DateTime? dateStart, DateTime? dateEnd);
        Task<IEnumerable<ActivityListModel>> GetActivitiesByUserCreatedAsync(Guid userId);
        Task<IEnumerable<ActivityListModel>> GetActivitiesByUserAssignedAsync(Guid userId);
        Task<IEnumerable<ActivityListModel>> GetActivitiesByDateLazyAsync(LazyDateType typeDate);
        Task DeleteUserActivitiesOnProject(Guid projectId, Guid userId);
    }
}
