using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void Prepopulation(IApplicationBuilder app, bool IsProduction)
        {
            using (var scopeService = app.ApplicationServices.CreateScope())
            {
                SeedData(scopeService.ServiceProvider.GetService<AppDbContext>(), IsProduction);
            }
        }

        private static void SeedData(AppDbContext context, bool IsProduction)
        {
            if (IsProduction)
            {
                Console.WriteLine("----> Start Database Migration");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"-----> Cannot migrate the database with error: {ex.Message}");
                }
            }
            if (!context.PlatForms.Any())
            {
                Console.WriteLine("---> starting to add data!");
                context.PlatForms.AddRange(
                    new Models.PlatForm() { Name = "dotnet", Publisher = "Microsoft", Cost = "free" },
                    new Models.PlatForm() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "free" },
                    new Models.PlatForm() { Name = "Kubernetes", Publisher = "Cloud Native Computer Foundation", Cost = "free" }

                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> we already have data!");
            }
        }
    }
}