using Domain.Entidades.Contacto;
using Domain.Util;
using Services.IServices.Contacto;
using System.Net.Mail;

namespace Services.Services.Contacto
{
    public class EmailEnvioService: IEmailEnvioService
    {
        public ResponseHelper enviarEmail(ContactoVM model)
        {
            var response = new ResponseHelper();
            if (string.IsNullOrEmpty(model.OpcionContacto))
            {
                response.Success = false;
                response.Message = "La opcion de contacto no puede ir vacia";
                return response;
            }

            if(model.OpcionContacto == "telefono")
            {
                if(string.IsNullOrEmpty(model.Fecha) || string.IsNullOrEmpty(model.Hora) || string.IsNullOrEmpty(model.Telefono))
                {
                    response.Success = false;
                    response.Message = "El campo Fecha, Hora o Teléfono no pude o pueden ir vacios, por favor revise sus datos y vuelva a intentarlo";
                    return response;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.Email))
                {
                    response.Success = false;
                    response.Message = "El campo Email no puede ir vacio, por favor revise sus datos y vuelva a intentarlo";
                    return response;
                }
            }
            
            var emailOrigen = "caamalmarco99@gmail.com";
            var emailDestino = "caamalmarco99@gmail.com";
            var password = "qjjvswzhgxdqvhju";

            var contenido = "<html>";
            contenido += "<p>Tienes un nuevo mensaje</p>";
            contenido += $"<p>Nombre: {model.Nombre}</p>";


            if(model.OpcionContacto == "telefono")
            {
                contenido += "<p>Eligió ser contactado por Teléfono:</p>";
                contenido += $"<p>Teléfono: {model.Telefono}</p>";
                contenido += $"<p>Fecha de Contacto: {model.Fecha}</p>";
                contenido += $"<p>Hora: {model.Hora}</p>";
            }
            else
            {
                contenido += "<p>Eligió ser contactado por Email:</p>";
                contenido += $"<p>Email: {model.Email}</p>";
            }

            contenido += @$"<p>Vende o Compra: {(model.OpcionVendeCompra == Common.Enums.Enums.OpcionCompraVenta.Vende ? "Vende" : "Compra")}</p>";
            contenido += $"<p>Precio o Presupuesto: {model.PrecioPresupuesto}</p>";

            MailMessage mailMessage = new MailMessage(emailOrigen, emailDestino, "Nuevo mensaje", contenido);
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(emailDestino, password);

            smtpClient.Send(mailMessage);
            smtpClient.Dispose();

            response.Success = true;
            response.Message = "Mensaje Enviado Correctamente, muy pronto me pondre en contacto contigo";
            return response;
        }
    }
}
