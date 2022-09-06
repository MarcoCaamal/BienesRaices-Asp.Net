using Domain.Entidades.Propiedades;
using Domain.Entidades.Vendedores;

namespace Domain.Entidades.Cuentas.ViewModels
{
    public class IndexAdminVM
    {
        public IEnumerable<Propiedad> Propiedades { get; set; }
        public IEnumerable<Vendedor> Vendedores { get; set; }
    }
}
