using AppCore.Utilities.Mapper;
using GalleryApp.DataAccess.EF.Concrete.Context;
using GalleryApp.DataAccess.MongoDB;
using GalleryApp.DataAccess.UOW;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryApp.Infrastructure.DI
{
    public static class DbInstaller
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();
            services.AddDbContext<GalleryDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<MongoDbContext>();
            return services;
        }
    }
}
