// Program.cs
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.PipelineBehaviors;
using PlatformService.Queries;
using PlatformService.Validation;

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    services.AddMediatR(cfg =>
    {
        // Scan the assembly that contains any handler or query class
        cfg.RegisterServicesFromAssemblyContaining<GetAllPlatformsQuery>();
    });
    
    services.AddValidatorsFromAssemblyContaining<PlatformCreateCommandValidator>();

    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

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
    

    app.UseHttpsRedirection();
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
