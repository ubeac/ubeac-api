using Identity.EntityFramework.API.Models;
using Identity.EntityFramework.API.Repositories;
using uBeac.Repositories;
using uBeac.Services;

namespace Identity.EntityFramework.API.Services
{
    public interface ICourseService : IEntityService<Course>
    {

    }
    public class CourseService : EntityService<Course>, ICourseService
    {
        public CourseService(ICourseRepository repository) : base(repository)
        {
        }

    }
}
