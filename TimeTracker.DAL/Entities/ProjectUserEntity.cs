namespace TimeTracker.DAL.Entities;

public class ProjectUserEntity : IEntity
{
    public required Guid UserEntityId { get; set; }
    public UserEntity UserEntity { get; set; }
    
    public required Guid ProjectEntityId { get; set; }
    public ProjectEntity ProjectEntity { get; set; }

}