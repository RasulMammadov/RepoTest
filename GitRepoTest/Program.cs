using GitRepoTest.gRPC.Services;
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

            builder.Services.AddMvcCore(opt =>
            {
              //  opt.SuppressInputFormatterBuffering = false;
            });
            builder.Services.AddControllers(opt =>
            {
                // opt.Filters.Add(IFilterMetadata customFilter);
            })
                .ConfigureApiBehaviorOptions(opt =>
                {
                    // opt.SuppressMapClientErrors = true;
                    opt.SuppressModelStateInvalidFilter = true;
                    opt.SuppressMapClientErrors = true;
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var customConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
                .Build();

            #region Static Files.
            var defaultFileOptions = new DefaultFilesOptions();
            defaultFileOptions.DefaultFileNames = customConfiguration.GetSection("DefaultFilesOptions").Get<List<string>>();

            app.UseDefaultFiles(defaultFileOptions);
            app.UseStaticFiles();
            app.UseFileServer();
            #endregion
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

            app.MapGrpcService<TestGrpcService>();

            app.Run();
        }
    }
}
