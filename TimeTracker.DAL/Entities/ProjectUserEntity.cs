namespace TimeTracker.DAL.Entities;

public record ProjectUserEntity : IEntity
{
    public required Guid UserEntityId { get; set; }
    public UserEntity? UserEntity { get; set; }
    
    public required Guid ProjectEntityId { get; set; }
    public ProjectEntity? ProjectEntity { get; set; }

    public Guid Id { get; set; }
}