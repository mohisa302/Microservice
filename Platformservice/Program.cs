using System.Reflection;      
using MediatR;                
// Program.cs
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.PipelineBehaviors;
using PlatformService.SyncDataServices.Http;
using PlatformService.Validation;
using PlatformService.Queries;
void ConfigureServices(
    IServiceCollection services,
    IWebHostEnvironment env,
    IConfiguration configuration)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    services.AddValidatorsFromAssemblyContaining<PlatformCreateCommandValidator>();
    services.AddMediatR(typeof(GetAllPlatformsQuery).Assembly);

    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
    services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
    // services.AddHttpClient<IweatherClinet, openWeatherClinet>(client =>
    // {
    // client.BaseAddress =  new Uri("");
    // });
    //in singleton class:
    //private readonly HttpClient _httpClient; use this for get and ... methods.
    if (env.IsProduction())
    {
         services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PlatformsConn")));
        Console.WriteLine("--> Using SQL db.");

    }
    else
    {

        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("InMem"));
        Console.WriteLine("--> Using In memeory db.");
    }

    services.AddScoped<IPlatformRepo, PlatformRepo>();
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();

        app.UseSwaggerUI();
    }

    // app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    PrepDb.PrepPupulation(app, env.IsProduction());

}
var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var configuration = builder.Configuration;
ConfigureServices(builder.Services, env, configuration);

var app = builder.Build();

Configure(app, app.Environment);


app.Run();
