using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteca.Services
{
    public static class EmailService
    {
        public static bool ValidaEnderecoEmail(string enderecoEmail)
        {
            try
            {
                string texto_Validar = enderecoEmail;
                Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

                if (expressaoRegex.IsMatch(texto_Validar))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool EnviaEmail(string Destinatario, string Assunto, string Mensagem)
        {
            try
            {
                if (!ValidaEnderecoEmail(Destinatario))
                    return  false;

                string Email = "projetobibliotecaunirp@outlook.com";
                string Senha = "projeto01041998";

                MailMessage mensagemEmail = new MailMessage(Email, Destinatario, Assunto, Mensagem);

                SmtpClient client = new SmtpClient("smtp.office365.com", 587);
                client.UseDefaultCredentials = false;

                NetworkCredential cred = new NetworkCredential(Email, Senha);
                client.Credentials = cred;
                
                client.EnableSsl = true;
                client.Timeout = 600000;
                client.Send(mensagemEmail);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
