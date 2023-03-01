namespace TimeTracker.DAL.Entities
{
    public class UserEntity : IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgUri { get; set; }

        public List<ActivityEntity> Activities { get; set; }
        public ICollection<ProjectUserEntity> Projects { get; set; }

    }
}