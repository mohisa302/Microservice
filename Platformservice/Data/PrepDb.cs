using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class PrepDb
  {
    public static void PrepPupulation(IApplicationBuilder app, bool isProduction)
    {
      using (var serviceScopre = app.ApplicationServices.CreateScope())
      {
        SeedData(serviceScopre.ServiceProvider.GetService<AppDbContext>(), isProduction);
      }
    }
    private static void SeedData(AppDbContext context, bool isProduction)
    {
      
        if (isProduction)
        {
          Console.WriteLine("-->  Apply migrations...");
        try
        {
          context.Database.Migrate();
        }
        catch (Exception ex)
        {
           Console.WriteLine($"-->Error in migrations {ex.Message}");

        }
        }
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
