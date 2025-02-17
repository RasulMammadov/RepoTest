using Microsoft.AspNetCore.Mvc.Filters;

namespace GitRepoTest.Helpers.Filters
{
    public class CustomActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

            Console.WriteLine("ActionFilterExecuted!!!");
            Console.WriteLine("ActionFilterExecuted!!!");
            Console.WriteLine("ActionFilterExecuted!!!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("ActionFilter!!!");
            Console.WriteLine("ActionFilter!!!");
            Console.WriteLine("ActionFilter!!!");
        }
    }
}
