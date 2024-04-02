using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackOverflowTags.Domain.Interfaces;
using StackOverflowTags.Infrastructure.Loggers;
using StackOverflowTags.Infrastructure.Persistence;
using StackOverflowTags.Infrastructure.Repositories;

namespace StackOverflowTags.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("TagsContext");

            services.AddDbContext<StackOverflowTagsDbContext>(
                options =>
                {
                    options.UseSqlServer(connString);
                });

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagTableInitializer, TagTableInitializer.TagTableInitializer>();

            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
