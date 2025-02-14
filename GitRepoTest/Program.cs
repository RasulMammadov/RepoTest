
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
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


            builder.Services.AddDirectoryBrowser();

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                   // opt.SuppressMapClientErrors = true;
                    opt.SuppressModelStateInvalidFilter = true;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseFileServer();

            app.UseExceptionHandler(Exc =>
            {
                Exc.Run(async httpContext =>
                {
                    var error = httpContext.Features.GetRequiredFeature<IExceptionHandlerFeature>().Error.Message;

                    await httpContext.Response.WriteAsync(error);
                    return;
                });
            });
            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseAuthorization();

         

            app.MapControllers();

            app.Run();
        }
    }
}
