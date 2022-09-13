using Identity.EntityFramework.API.Models;
using Identity.EntityFramework.API.Services;
using Microsoft.AspNetCore.Mvc;
using uBeac.Web;

namespace Identity.EntityFramework.API.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _service;
        public CourseController(ICourseService service)
        {
            _service= service;
        }

        [HttpPost]
        public async Task<IResult<Guid>> Create([FromBody] Course request, CancellationToken cancellationToken = default)
        {
            await _service.Create(request, cancellationToken);
            return request.Id.ToResult();
        }

        [HttpPost]
        public async Task Update([FromBody] Course request, CancellationToken cancellationToken = default)
            => await _service.Update(request, cancellationToken);

        [HttpPost]
        public async Task Delete([FromBody] IdRequest request, CancellationToken cancellationToken = default)
            => await _service.Delete(request.Id, cancellationToken);

        //[HttpPost]
        //public async Task<IListResult<Student>> Search([FromBody] SearchFaqRequest request, CancellationToken cancellationToken = default)
        //    => await _service.Search(request, cancellationToken).ToListResultAsync(cancellationToken);

        [HttpPost]
        public async Task<Course> GetById([FromBody] IdRequest request, CancellationToken cancellationToken = default)
            => await _service.GetById(request.Id, cancellationToken);

        [HttpGet]
        public async Task<List<Course>> GetAll(CancellationToken cancellationToken = default)
            => (await _service.GetAll(cancellationToken)).ToList();
    }
}
