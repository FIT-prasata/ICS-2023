using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.BL.Mappers.Interfaces;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
namespace TimeTracker.BL.Mappers
{
    public class ProjectModelMapper: IModelMapper<ProjectEntity, ProjectDetailModel, ProjectListModel>

    {

        private readonly ActivityModelMapper _activityModelMapper;
        private readonly ProjectUserModelMapper _projectUserModelMapper;
        public ProjectModelMapper(ActivityModelMapper activityModelMapper, ProjectUserModelMapper projectUserModelMapper)
        {
            _activityModelMapper = activityModelMapper;
            _projectUserModelMapper = projectUserModelMapper;
        }


        public ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
         => entity is null
             ? ProjectDetailModel.Empty
             : new ProjectDetailModel
             {
                 Id = entity.Id,
                 Name = entity.Name,
                 Description = entity.Description,
                 CreatedById = entity.CreatedById,
                 Activities = _activityModelMapper.MapToListModel(entity.Activities).ToObservableCollection(),
                 Users = _projectUserModelMapper.MapToListModel(entity.Users).ToObservableCollection(),
             };   

        public ProjectEntity MapToEntity(ProjectDetailModel model)
        => new()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            CreatedById = model.CreatedById,
        };

        public ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null ? ProjectListModel.Empty
            : new ProjectListModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
    }
}
