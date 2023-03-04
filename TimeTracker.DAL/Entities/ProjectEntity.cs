namespace TimeTracker.DAL.Entities;

public class ProjectEntity : IEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required Guid CreatedById { get; set; }

    public UserEntity? CreatedBy { get; set; }

    public List<ActivityEntity>? Activities { get; set; }
    public List<ProjectUserEntity>? Users { get; set; }
}