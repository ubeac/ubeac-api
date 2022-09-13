using System.ComponentModel.DataAnnotations;

namespace Identity.EntityFramework.API.Models
{
    public class Student : Entity
    {
        public Student()
        {
            Courses = new HashSet<Course>();
            Docs = new HashSet<Doc>();
        }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string StudentName { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Doc> Docs { get; set; }
    }
}
