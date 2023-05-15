using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTracker.DAL.Entities;
using TimeTracker.BL.Models;
namespace TimeTracker.BL.Mappers
{
    public class UserModelMapper: ModelMapperBase<UserEntity,UserListModel, UserDetailModel>, IUserModelMapper
    {
        public override UserDetailModel  MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                ImgUri = entity.ImgUri,
            };
        public override UserEntity MapToEntity(UserDetailModel model)
            => new()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ImgUri = model.ImgUri,
            };

        public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                ImgUri = entity.ImgUri,
            };

        public IEnumerable<UserListModel> MapToListModel(IEnumerable<UserEntity>? entities)
            => entities is null ? Enumerable.Empty<UserListModel>() :
                entities.Select(MapToListModel);


    }
}
