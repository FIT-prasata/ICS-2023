namespace TimeTracker.DAL.Entities;

public class ProjectEntity : IEntity
{
    public required string Name { get; set; }

    public List<ActivityEntity> Activities { get; set; }
    public List<ProjectUserEntity> Users { get; set; }
}