
using Microsoft.AspNetCore.Mvc.Filters;

namespace GitRepoTest.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
    {
        Console.WriteLine("Action --- ActionFilterAttribute!!!");
        Console.WriteLine("Action --- ActionFilterAttribute!!!");
        Console.WriteLine("Action --- ActionFilterAttribute!!!");

        base.OnActionExecuting(context);
    }
        public override void OnResultExecuting(ResultExecutingContext context)
        {

            Console.WriteLine("Result --- ActionFilterAttribute!!!");
            Console.WriteLine("Result --- ActionFilterAttribute!!!");
            Console.WriteLine("Result --- ActionFilterAttribute!!!");
            base.OnResultExecuting(context);
        }
    }
}