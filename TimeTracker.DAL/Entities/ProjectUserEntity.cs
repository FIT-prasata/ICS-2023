namespace TimeTracker.DAL.Entities;

public class ProjectUserEntity : IEntity
{
    public UserEntity UserEntity { get; set; }
    public ProjectEntity ProjectEntity { get; set; }

    public List<ActivityEntity> Activities { get; set; }
}