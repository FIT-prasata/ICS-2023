using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.FirstName = newEntity.FirstName;
        existingEntity.LastName = newEntity.LastName;
        existingEntity.ImgUri = newEntity.ImgUri;
    }
}
