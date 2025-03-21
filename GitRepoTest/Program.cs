<<<<<<< HEAD

=======
using System.Reflection;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using static GitRepoTest.gRPC.TestService;
using GitRepoTest.gRPC.Services;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder.Extensions;
>>>>>>> af58e5457007201330dad52965d8752076456473
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(opt =>
                {
                   // opt.SuppressMapClientErrors = true;
                    opt.SuppressModelStateInvalidFilter = true;
                });
=======
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


>>>>>>> af58e5457007201330dad52965d8752076456473
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

<<<<<<< HEAD
=======
            builder.Services.AddGrpc();

            builder.Services.AddScoped(typeof(ICheck<,>), typeof(Check<,>));


            builder.Services.AddTransient<IMyService, FirstService>();
            builder.Services.AddTransient<IMyService, SecondService>();

            var currentDirectory = Environment.CurrentDirectory;

            var assemblyLocation = System.Reflection.Assembly.GetExecutingAssembly().Location;


            var directoryOfAssembly = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var mess = new Message();

            var multimess = new MulticastMessage();


            /*var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firebaseKey.json")),
            });*/
>>>>>>> af58e5457007201330dad52965d8752076456473

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

<<<<<<< HEAD
            app.UseStaticFiles();
            app.UseFileServer();

=======
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
>>>>>>> af58e5457007201330dad52965d8752076456473
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

            app.MapGet("/GetTestDI", (HttpContext httpContext, IMyService myServices) =>
            {
                return IMyService.Id;
            });

            app.Run();
        }

        interface ICheck<T, @int>
        {
        }

        class Check<T, TT> : ICheck<T, TT>
        {
        }
        public interface IMyService
        {
            static int Id;
            void Execute();
        }

        public class FirstService : IMyService
        {
            public FirstService()
            {
                IMyService.Id = 34;
            }

            public void Execute() => Console.WriteLine("First Service");
        }

        public class SecondService : IMyService
        {
            public SecondService()
            {

            }
            public void Execute() => Console.WriteLine("Second Service");
        }


    }
}
