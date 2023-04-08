namespace TimeTracker.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public required Guid CreatedById { get; set; }

    public UserEntity? CreatedBy { get; set; }

    public IEnumerable<ActivityEntity>? Activities { get; set; }
    public IEnumerable<ProjectUserEntity>? Users { get; set; }
    public Guid Id { get; set; }
}