using Echooling.Aplication.Abstraction.Services;
using Echooling.Aplication.Abstraction.Storage;
using Echooling.Infrastructure.Services.Storage;
using Echooling.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
namespace Echooling.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastuctureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IStorageService,StorageService>();
        }
        public static void addStorage<T>(this IServiceCollection services) where T: class ,IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
