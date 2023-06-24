using CowboyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CowboyAPI
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CowboyDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<CowboyDbContext>>()))
            {
                // Look for any board games.
                if (context.Cowboys.Any())
                {
                    return;   // Data was already seeded
                }

                context.Cowboys.AddRange(
                    new Cowboy
                    {
                        Id = 1,
                        Name = "Tom",
                        Height = 170,
                        Hair = "brown",
                    },
                    new Cowboy
                    {
                        Id = 2,
                        Name = "Jerry",
                        Height = 184,
                        Hair = "blue",
                    });

                context.SaveChanges();
            }
        }
    }
}
