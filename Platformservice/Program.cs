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
void ConfigureServices(IServiceCollection services)
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

    services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("InMem"));

    services.AddScoped<IPlatformRepo, PlatformRepo>();
}

void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{

        app.UseDeveloperExceptionPage();
        app.UseSwagger();

        app.UseSwaggerUI();
    

    // app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
    PrepDb.PrepPupulation(app);

}
var builder = WebApplication.CreateBuilder(args);


ConfigureServices(builder.Services);

var app = builder.Build();
Configure(app, app.Environment);


app.Run();
