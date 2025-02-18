using Microsoft.AspNetCore.Mvc;

namespace GitRepoTest.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("/GetTest")]
        public ActionResult GetTest()
        {
            var request = HttpContext.Request;
            return Ok(new { Message = "this is get test method", Sender = "You do not need to know" });
        }

        [HttpPost]
        [Route("/PostTest")]
        public ActionResult PostTest()
        {

            var request = HttpContext.Request;
            return Ok(new { Message = "this is post test method", Sender = "You do not need to know" });
        }

        [HttpPost]
        [Route("/Redirect")]
        public IActionResult PostTest2()
        {

            return Redirect("https://localhost:7047/GetTest");
        }
    }
}