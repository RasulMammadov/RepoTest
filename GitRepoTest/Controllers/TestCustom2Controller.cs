using Microsoft.AspNetCore.Mvc;

namespace GitRepoTest.Controllers
{
    [ApiController]
    [Route("TestCustom2")]
    public class TestCustom2Controller : ControllerBase
    {
        [HttpPost("/TestcustomRequest")]
        public IActionResult Index(Request request)
        {
           var req = HttpContext.Request;
            return Ok(new { Id = request.id, Name = request.Name, ID2 = 35, Name2 = "DUDD", Course = 3 });
        }
        public class Request
        {
            public int id { get; set; }
            public string Name { get; set; }
        }
    }
}
