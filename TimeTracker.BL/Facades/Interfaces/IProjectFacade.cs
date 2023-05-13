using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Models;
using TimeTracker.DAL.Entities;

namespace TimeTracker.BL.Facades.Interfaces
{
    public interface IProjectFacade: IFacade<ProjectEntity, ProjectDetailModel, ProjectListModel>
    {
        Task AddUserToProjectAsync(Guid projectId, Guid userId);
        Task RemoveUserFromProjectAsync(Guid projectId, Guid userId);
    }
}
