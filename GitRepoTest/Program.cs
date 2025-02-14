
using GitRepoTest.Helpers.Filters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Serilog;

namespace GitRepoTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<CustomResultFilter>();
            builder.Services.AddSingleton<SingletonClassTest>();
            builder.Services.AddAntiforgery();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            /*app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseFileServer();*/
            app.UseAntiforgery();
            app.UseSerilogRequestLogging();

            app.UseAuthorization();


           


            

            app.UseCors(policy =>
            {
                policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
            });
            app.UseExceptionHandler(conf =>
            {

                conf.Run(async httpcontext =>
                {
                    IExceptionHandlerFeature exception = httpcontext.Features.Get<IExceptionHandlerFeature>(); //GetRequiredFeature<>
                    httpcontext.Response.ContentType = "application/json";
                    httpcontext.Response.StatusCode = StatusCodes.Status202Accepted;
                    httpcontext.Response.Headers.Append("TestingException", "++++++++++++++++++++++++++");

                    if (exception is null)
                    {
                        await httpcontext.Response.WriteAsJsonAsync(new { Problem = "I am Batman" });
                        return;
                    }
                    await httpcontext.Response.WriteAsJsonAsync(exception.Error.StackTrace);
                    return;
                });
            });
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // ...
            });

            app.MapGet("/Gettoli", (IWebHostEnvironment env, ILogger<Program> _logger) =>
            {
                string Value1 = @"Hello \nWorld ASFfg\tAze\rh\\df";
                string Value2 = "File: \"Important\".pdf klbddfs";


                Console.WriteLine("");
                Console.WriteLine("++++++++++++++++");
            });
            app.MapControllers();
            app.Run();
        }
    }
}
