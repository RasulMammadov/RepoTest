using Microsoft.AspNetCore.Mvc;
using static GitRepoTest.Program;

namespace GitRepoTest.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class TestController : ControllerBase
    {

        public TestController(Singleton singleton, ILogger<TestController> logger)
        {
            this.singleton = singleton;
            mutex = new Mutex(true, "MyMutex", out checkmutex);
            semaphore = new Semaphore(0,1, "MySemaphore");
            _logger = logger;
        }

        private static readonly Object _lockObject = new Object();
        private readonly Singleton singleton;
        private Mutex mutex;
        private static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly ILogger<TestController> _logger;
        private Semaphore semaphore;
        private bool checkmutex;

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
        public async Task<IActionResult> PostTest2()
        {

            await semaphoreSlim.WaitAsync();

            Thread.Sleep(5000);

            _logger.LogError("Semophore Slim Multiple Processes Test");

            semaphoreSlim.Release();

            var state = Thread.CurrentThread.ThreadState;

            var num = singleton.number;
            singleton.number = 0;

            return Redirect("https://localhost:7047/GetTest");
        }
    }
}