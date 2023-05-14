using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
namespace TimeTracker.BL.Mappers
{
    public class ProjectModelMapper: ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>, IProjectModelMapper
    {

        private readonly IActivityModelMapper _activityModelMapper;
        private readonly IUserModelMapper _userModelMapper;
        public ProjectModelMapper(IActivityModelMapper activityModelMapper, IUserModelMapper userModelMapper)
        {
            _activityModelMapper = activityModelMapper;
            _userModelMapper = userModelMapper;
        }


        public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
         => entity is null
             ? ProjectDetailModel.Empty
             : new ProjectDetailModel
             {
                 Id = entity.Id,
                 Name = entity.Name,
                 Description = entity.Description,
                 CreatedById = entity.CreatedById,
                 Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                 Users = entity.Users != null ? _userModelMapper.MapToListModel(entity.Users.Select(o => o.UserEntity)!).ToObservableCollection() : new List<UserListModel>().ToObservableCollection(),
             };   

        public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            CreatedById = model.CreatedById,
        };

        public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null ? ProjectListModel.Empty
            : new ProjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
    }
}
