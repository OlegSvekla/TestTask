using Microsoft.EntityFrameworkCore;
using TestTask.Data;

namespace TestTask.ApplicationBuilderExtensions
{
    public static class SeedExtensions
    {
        public static IApplicationBuilder UseDbContextMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var logger = serviceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

                logger.LogInformation("Database migration running...");

                try
                {
                    var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            return app;
        }
    }
}
