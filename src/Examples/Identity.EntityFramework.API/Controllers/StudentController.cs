using Identity.EntityFramework.API.Models;
using Identity.EntityFramework.API.Services;
using Identity.EntityFramework.API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace Identity.EntityFramework.API.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IResult<Guid>> Create([FromBody] Student request, CancellationToken cancellationToken = default)
        {
            await _service.Create(request, cancellationToken);
            return request.Id.ToResult();
        }

        [HttpPost]
        public async Task Update([FromBody] Student request, CancellationToken cancellationToken = default)
            => await _service.Update(request, cancellationToken);

        [HttpPost]
        public async Task Delete([FromBody] IdRequest request, CancellationToken cancellationToken = default)
            => await _service.Delete(request.Id, cancellationToken);

        //[HttpPost]
        //public async Task<IListResult<Student>> Search([FromBody] SearchFaqRequest request, CancellationToken cancellationToken = default)
        //    => await _service.Search(request, cancellationToken).ToListResultAsync(cancellationToken);

        [HttpPost]
        public async Task<Student> GetById([FromBody] IdRequest request, CancellationToken cancellationToken = default)
            => await _service.GetById(request.Id, cancellationToken);

        [HttpGet]
        public async Task<List<StudentVM>> GetAll(CancellationToken cancellationToken = default)
        {
            var z = (await _service.GetAll(cancellationToken)).ToList();

            var items = z.Select(x => new StudentVM
            {
                Id = x.Id,
                StudentName = x.StudentName,
                UserId = x.UserId,
                Courses = x.Courses.ToList(),
                Docs = x.Docs.ToList()
            }).ToList();

            items.ForEach(item => item.Courses.ForEach(y => y.Students = null));

            return items;
        }
    }
}
