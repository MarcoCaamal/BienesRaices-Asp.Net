using Microsoft.Extensions.DependencyInjection;
using Services.IServices;
using Services.IServices.Propiedades;
using Services.IServices.Vendedores;
using Services.Services;
using Services.Services.Propiedades;
using Services.Services.Vendedores;

namespace Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesLayer(this IServiceCollection services)
        {
            services.AddTransient<IAlmacenadorArchivosService, AlmacenadorArchivosService>();
            services.AddTransient<IPropiedadesService, PropiedadesService>();
            services.AddTransient<IVendedoresService, VendedoresService>();
        }
    }
}
