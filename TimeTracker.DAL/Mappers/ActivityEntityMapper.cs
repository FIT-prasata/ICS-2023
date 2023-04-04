using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.Description = newEntity.Description;
        existingEntity.Type = newEntity.Type;
        existingEntity.CreatedById = newEntity.CreatedById;
        existingEntity.AssignedId = newEntity.AssignedId;
        existingEntity.ProjectId = newEntity.ProjectId;
    }
}
