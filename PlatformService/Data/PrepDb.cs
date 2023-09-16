using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void  PrepPopulation(IApplicationBuilder app, bool isProd)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
            }
        }

        private static void SeedData(AppDbContext context,  bool isProd)
        {
            if(isProd){
                context.Database.Migrate();
            }

            if(!context.Platforms.Any())
            {
                Console.WriteLine("----Seeding Data");

                context.Platforms.AddRange(

                    new Platform() {
                        Name = "Dot Net", Publisher = "Microsoft", Cost = "Free"
                    },
                     new Platform() {
                        Name = "Sql Server Express", Publisher = "Microsoft", Cost = "Free"
                    },
                     new Platform() {
                        Name = "Kubernetes", Publisher = "Cloud Native", Cost = "Free"
                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data");
            }
        }
    }
    
}