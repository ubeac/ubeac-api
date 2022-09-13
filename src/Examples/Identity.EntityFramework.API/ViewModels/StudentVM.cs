using Identity.EntityFramework.API.Models;

namespace Identity.EntityFramework.API.ViewModels
{
    public class StudentVM
    {
        public Guid Id { get; set; }
        public List<Course> Courses { get; set; }
        public List<Doc> Docs { get; set; }
        public Guid UserId { get; set; }        
        public string StudentName { get; set; }
    }
}
