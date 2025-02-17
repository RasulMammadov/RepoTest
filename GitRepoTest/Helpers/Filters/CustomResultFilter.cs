using Microsoft.AspNetCore.Mvc.Filters;

namespace GitRepoTest.Helpers.Filters
{
    public class CustomResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {

            Console.WriteLine("ResultFilterExecuted!!!");
            Console.WriteLine("ResultFilterExecuted!!!");
            Console.WriteLine("ResultFilterExecuted!!!");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("ResultFilter!!!");
            Console.WriteLine("ResultFilter!!!");
            Console.WriteLine("ResultFilter!!!");
        }
    }
}
