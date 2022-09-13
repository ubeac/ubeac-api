using Identity.EntityFramework.API.Models;
using Microsoft.EntityFrameworkCore;
using uBeac.Repositories;
using uBeac.Repositories.EntityFramework;
using uBeac.Repositories.History;

namespace Identity.EntityFramework.API.Repositories
{
    public interface IStudentRepository : IEntityRepository<Student>
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class StudentRepository<TContext> : EFEntityRepository<Student, TContext>, IStudentRepository where TContext : EFDbContext
    {
        public StudentRepository(TContext dbContext, IApplicationContext applicationContext, IHistoryManager historyManager) : base(dbContext, applicationContext, historyManager)
        {
        }

        public override async Task<IEnumerable<Student>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(AsQueryable().Include(x => x.Courses).Include(x => x.Docs).ToList());
        }
    }
}
