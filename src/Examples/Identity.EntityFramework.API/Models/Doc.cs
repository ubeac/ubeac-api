namespace Identity.EntityFramework.API.Models
{
    public class Doc : Entity
    {
        public string Name { get; set; }
        public Guid StudentId { get; set; }
    }
}
