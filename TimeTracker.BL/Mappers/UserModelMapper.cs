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
    public class UserModelMapper: IModelMapper<UserEntity,UserDetailModel,UserListModel>
    {
        public UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                ImgUri = entity.ImgUri,
            };
        public UserEntity MapToEntity(UserDetailModel model)
            => new()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImgUri = model.ImgUri,
            };

        public UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };

        public IEnumerable<UserListModel> MapToListModel(IEnumerable<UserEntity>? entities)
            => entities is null ? Enumerable.Empty<UserListModel>() :
                entities.Select(MapToListModel);


    }
}
