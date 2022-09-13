
namespace Identity.EntityFramework.API.Models
{
    public partial class Course : Entity
    {
        public Course()
        {
            Students = new HashSet<Student>();
        }
        public string Name { get; set; }        
        public virtual ICollection<Student> Students { get; set; }
    }
}
