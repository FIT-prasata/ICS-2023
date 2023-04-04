using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mapper;
public interface IEntityMapper<in TEntity>
    where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
