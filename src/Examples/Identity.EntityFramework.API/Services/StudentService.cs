using Identity.EntityFramework.API.Models;
using Identity.EntityFramework.API.Repositories;
using uBeac.Services;

namespace Identity.EntityFramework.API.Services
{
    public interface IStudentService : IEntityService<Student>
    {        
    }
    public class StudentService : EntityService<Student>, IStudentService
    {
        private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task Create(Student entity, CancellationToken cancellationToken = default)
        {
             await base.Create(entity, cancellationToken);
            await _repository.SaveChangesAsync();
        }
    }
}
