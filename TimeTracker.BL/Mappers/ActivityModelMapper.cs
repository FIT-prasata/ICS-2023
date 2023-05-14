using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;

namespace TimeTracker.BL.Mappers
{
    public class ActivityModelMapper: ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>, IActivityModelMapper
    {
        private readonly IUserModelMapper _userModelMapper;

        public ActivityModelMapper(IUserModelMapper userModelMapper)
        {
            _userModelMapper = userModelMapper;
        }

        public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
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

        public override ActivityEntity MapToEntity(ActivityDetailModel model)
            => new()
            {
                Id = model.Id,
                Start = model.Start,
                End = model.End,
                Description = model.Description ?? string.Empty,
                Type = model.Type,
                CreatedById = model.CreatedBy.Id,
                AssignedId = model.Assigned.Id == Guid.Empty ? null : model.Assigned.Id,
                ProjectId = model.ProjectId,
            };

        public override ActivityListModel MapToListModel(ActivityEntity? entity)
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
