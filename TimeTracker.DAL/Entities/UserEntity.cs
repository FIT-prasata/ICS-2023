namespace TimeTracker.DAL.Entities
{
    public record UserEntity : IEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? ImgUri { get; set; }

        public List<ActivityEntity>? Activities { get; set; }
        
        public List<ActivityEntity>? AuthoredActivities { get; set; }
        public List<ProjectUserEntity>? Projects { get; set; }


        public Guid Id { get; set; }
    }
}