﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
namespace TimeTracker.BL.Mappers
{
    public class ProjectUserModelMapper: IModelMapper<ProjectUserEntity, ProjectUserDetailModel, ProjectUserListModel >
    {
        public ProjectUserListModel MapToListModel(ProjectUserEntity? entity)
            => entity is null
                ? ProjectUserListModel.Empty
                : new ProjectUserListModel()
                {
                    Id = entity.Id,
                    ProjectId = entity.ProjectEntityId,
                    UserId = entity.UserEntityId,
                };
        public IEnumerable<ProjectUserListModel> MapToListModel(IEnumerable<ProjectUserEntity>? entities)
            => entities is null ? Enumerable.Empty<ProjectUserListModel>() :
                entities.Select(MapToListModel);

        public ProjectUserDetailModel MapToDetailModel(ProjectUserEntity? entity)
        {
            throw new NotImplementedException("This won't be necessary to implement");
        }

        public ProjectUserEntity MapToEntity(ProjectUserDetailModel model)
        {
            throw new NotImplementedException("This won't be necessary to implement");
        }
    }
}


