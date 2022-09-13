using Identity.EntityFramework.API.Models;
using System.Data.Entity;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace Identity.EntityFramework.API.Repositories
{
    public interface ICourseRepository : IEntityRepository<Course>
    {

    }

    public class CourseRepository<TContext> : EFEntityRepository<Course, TContext>, ICourseRepository where TContext : EFDbContext
    {
        public CourseRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
        {
        }

        public override async Task<IEnumerable<Course>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(AsQueryable().Include(x => x.Students).ToList());            
        }
    }
}
