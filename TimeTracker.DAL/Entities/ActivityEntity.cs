using TimeTracker.DAL.Enums;

namespace TimeTracker.DAL.Entities;

public class ActivityEntity : IEntity
{
    public required DateTime Start { get; set; }

    public required DateTime End { get; set; }

    public required string Description { get; set; }

    public required ActivityType Type { get; set; }

    public required Guid CreatedById { get; set; }
    public UserEntity? CreatedBy { get; set; }

    public Guid? AssignedId { get; set; }
    public UserEntity? Assigned { get; set; }

    public required Guid ProjectId { get; set; }
    public ProjectEntity? Project { get; set; }
}