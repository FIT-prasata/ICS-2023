using TimeTracker.DAL.Entities;

namespace TimeTracker.DAL.Mappers;

public class ProjectUserEntityMapper : IEntityMapper<ProjectUserEntity>
{
    public void MapToExistingEntity(ProjectUserEntity existingEntity, ProjectUserEntity newEntity)
    {
        existingEntity.UserEntityId = newEntity.UserEntityId;
        existingEntity.ProjectEntityId = newEntity.ProjectEntityId;
    }
}