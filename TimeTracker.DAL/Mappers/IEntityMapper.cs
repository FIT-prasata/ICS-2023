using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;
public interface IEntityMapper<in TEntity>
    where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
