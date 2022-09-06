using Domain.Entidades.Contacto;
using Domain.Util;

namespace Services.IServices.Contacto
{
    public interface IEmailEnvioService
    {
        ResponseHelper enviarEmail(ContactoVM model);
    }
}
