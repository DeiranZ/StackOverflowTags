using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackOverflowTags.Infrastructure.Persistence;

namespace StackOverflowTags.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("ReceptionistContext");

            services.AddDbContext<StackOverflowTagsDbContext>(options =>
                options.UseMySQL(connString));
        }
    }
}
