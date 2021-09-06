using GalleryApp.Business.Abstract;
using GalleryApp.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace GalleryApp.Infrastructure.DI
{
    public static class ServiceInstaller
    {

        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISliderService, SliderService>();
            return services;
        }
    }
}
