using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackOverflowTags.Domain.Interfaces;
using StackOverflowTags.Infrastructure.Persistence;
using StackOverflowTags.Infrastructure.Repositories;

namespace StackOverflowTags.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("TagsContext");
            var serverVersion = ServerVersion.Parse(configuration.GetConnectionString("ServerVersion"));

            services.AddDbContext<StackOverflowTagsDbContext>(
                options =>
                {
                    options.UseMySql(connString, serverVersion).LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
                });

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagTableInitializer, TagTableInitializer.TagTableInitializer>();
        }
    }
}
