using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPupulation(IApplicationBuilder app)
    {
      using (var serviceScopre = app.ApplicationServices.CreateScope())
      {
        SeedData(serviceScopre.ServiceProvider.GetService<AppDbContext>());
      }
    }
    private static void SeedData(AppDbContext context)
    {
      if (!context.Platforms.Any())
      {
        Console.WriteLine("-->  Seeding Data...");
        context.Platforms.AddRange(
          new Platform() { Name = "Dotnet", Publisher = "Microsoft", Cost = "Free" },
          new Platform() { Name = "Sql", Publisher = "Microsoft", Cost = "Free" },
          new Platform() { Name = "Kubernetes", Publisher = "Cloud", Cost = "Free" }
        );
        context.SaveChanges();
        
      }
      else
      {
        Console.WriteLine("-->  We already have data");
      }
    }
  }
}
