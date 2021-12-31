using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using uBeac.Web;

namespace TestApplication.Controllers
{
    public class TestController : BaseController
    {
        private readonly ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
            LogContext.PushProperty("_Custom", "111111111111111111111111111111111111");
        }

        [HttpGet]
        public int Add(int x = 1, int y = 2)
        {
            return _testService.Add(x, y);
        }

        [HttpPost]
        public TestModel Send([FromBody] TestModel testModel)
        {
            testModel.Id = new Random().Next(1000);
            return testModel;
        }
    }
}
