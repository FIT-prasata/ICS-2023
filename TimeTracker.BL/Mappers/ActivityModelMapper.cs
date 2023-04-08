using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Mappers
{
    public class ActivityModelMapper: IModelDetailMapper<ActivityEntity, ActivityDetailModel>, IModelListMapper<ActivityEntity, ActivityListModel>
    {
        public ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
            => entity is null
                ? ActivityDetailModel.Empty
                : new ActivityDetailModel
                {
                    Id = entity.Id,
                    Start = entity.Start,
                    End = entity.End,
                    Description = entity.Description,
                    Type = entity.Type,
                    CreatedById = entity.CreatedById,
                    AssignedId = entity.AssignedId,
                    ProjectId = entity.ProjectId,
                };

        public ActivityEntity MapToEntity(ActivityDetailModel model)
            => new()
            {
                Id = model.Id,
                Start = model.Start,
                End = model.End,
                Description = model.Description ?? string.Empty,
                Type = model.Type,
                CreatedById = model.CreatedById,
                AssignedId = model.AssignedId,
                ProjectId = model.ProjectId,
            };

        public ActivityListModel MapToListModel(ActivityEntity? entity)
            => entity is null
                ? ActivityListModel.Empty
                : new ActivityListModel()
                {
                    Id = entity.Id,
                    Start = entity.Start,
                    End = entity.End,
                    Type = entity.Type,
                    AssignedId = entity.AssignedId,
                    ProjectId = entity.ProjectId,
                };

        public IEnumerable<ActivityListModel> MapToListModel(IEnumerable<ActivityEntity>? entities)
            => entities is null ? Enumerable.Empty<ActivityListModel>() :
                entities.Select(MapToListModel);

    }
}
