using TimeTracker.DAL.Enums;

namespace TimeTracker.DAL.Entities;

public class ActivityEntity : IEntity
{
    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string Description { get; set; }

    public ActivityType Type { get; set; }

    public Guid CreatedById { get; set; }
    public UserEntity CreatedBy { get; set; }

    public Guid AssignedId { get; set; }
    public UserEntity Assigned { get; set; }

    public Guid ProjectId { get; set; }
    public ProjectEntity Project { get; set; }
}