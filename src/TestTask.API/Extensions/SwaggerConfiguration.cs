using Microsoft.OpenApi.Models;
using System.Reflection;

namespace TestTask.API.Extensions
{
    public static class SwaggerConfiguration
    {
        public static void Configuration(
            IServiceCollection services)
        {
            services.AddSwaggerGen();           
        }
    }
}