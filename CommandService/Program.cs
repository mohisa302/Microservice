// Program.cs
using Microsoft.EntityFrameworkCore;

void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    // services.AddDbContext<AppDbContext>(options =>
    //     options.UseInMemoryDatabase("InMem"));
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
    // PrepDb.PrepPupulation(app);

}
var builder = WebApplication.CreateBuilder(args);


ConfigureServices(builder.Services);

var app = builder.Build();
Configure(app, app.Environment);


app.Run();
