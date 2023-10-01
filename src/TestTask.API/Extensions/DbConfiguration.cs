using Microsoft.EntityFrameworkCore;
using TestTask.Infrastructure.Data;

namespace TestTask.API.Extensions
{
    public static class DbConfiguration
    {
        public static void Configuration(
            IConfiguration configuration,
            IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TestTaskConnection")));
        }
    }
}