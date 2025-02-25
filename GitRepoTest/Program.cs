using System.Reflection;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using static GitRepoTest.gRPC.TestService;
using GitRepoTest.gRPC.Services;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
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

         //   builder.Services.AddDirectoryBrowser();

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

            var currentDirectory = Environment.CurrentDirectory;

            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;


            var directoryOfAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var directoryname2 = AppContext.BaseDirectory;


            var directoryname3 = AppContext.TargetFrameworkName;



            var mess = new Message();

            var multimess = new MulticastMessage();


            /*var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebaseKey.json")),
            });*/

            builder.Services.AddSingleton<Singleton>();

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
            defaultFileOptions.RequestPath = "/Files";

            app.UseDefaultFiles(defaultFileOptions);
            app.UseStaticFiles("/Files");
           
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

            var id = Environment.GetEnvironmentVariable("APP_UID");

            app.Run();
        }

        public class Singleton
        {
            public int number = 13;
        }
    }
}
