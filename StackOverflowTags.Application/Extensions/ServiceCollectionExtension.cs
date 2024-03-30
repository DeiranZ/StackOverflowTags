using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using StackOverflowTags.Application.Mappings;

namespace StackOverflowTags.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(System.Reflection.Assembly.GetExecutingAssembly()));

            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                cfg.AddProfile(new TagMappingProfile());
            }).CreateMapper()
            );
        }
    }
}
