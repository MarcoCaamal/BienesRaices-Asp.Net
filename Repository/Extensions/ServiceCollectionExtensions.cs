using Microsoft.Extensions.DependencyInjection;
using Repository.IRepositories.Cuentas;
using Repository.IRepositories.Propiedades;
using Repository.IRepositories.Vendedores;
using Repository.Repositories.Cuentas;
using Repository.Repositories.Propiedades;
using Repository.Repositories.Vendedores;

namespace Repository.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoriesLayer(this IServiceCollection services)
        {
            services.AddTransient<IPropiedadesRepository, PropiedadesRepository>();
            services.AddTransient<IVendedoresRepository, VendedoresRepository>();
            services.AddTransient<IUsuariosRepository, UsuariosRepository>();
        }
    }
}
