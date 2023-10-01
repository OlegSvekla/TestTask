using TestTask.BL.Services.Implementation;
using TestTask.BL.Services.Interfaces;
using TestTask.Infrastructure.Data.Repository.Implementation;
using TestTask.Infrastructure.Data.Repository.IRepository;

namespace TestTask.API.Extensions
{
    public class ServicesConfiguration
    {
        public static void Configuration(
            IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }  
}