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
    public class ActivityModelMapper: IModelMapper<ActivityEntity, ActivityDetailModel, ActivityListModel>
    {
        private readonly UserModelMapper _userModelMapper;

        public ActivityModelMapper(UserModelMapper userModelMapper)
        {
            _userModelMapper = userModelMapper;
        }

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
                    CreatedBy = _userModelMapper.MapToDetailModel(entity.CreatedBy),
                    Assigned = _userModelMapper.MapToDetailModel(entity.Assigned),
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
                CreatedById = model.CreatedBy.Id,
                AssignedId = model.Assigned.Id,
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
                    Assigned = _userModelMapper.MapToDetailModel(entity.Assigned),
                };

        public IEnumerable<ActivityListModel> MapToListModel(IEnumerable<ActivityEntity>? entities)
            => entities is null ? Enumerable.Empty<ActivityListModel>() :
                entities.Select(MapToListModel);

    }
}
