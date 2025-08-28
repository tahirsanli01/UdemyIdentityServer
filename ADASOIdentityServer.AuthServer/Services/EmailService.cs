using ADASOIdentityServer.AuthServer.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ADASOIdentityServer.AuthServer.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = "smtp.office365.com";
        private readonly int _port = 587;
        private readonly string _fromEmail = "bilgi-islem@adaso.org.tr";
        private readonly string _password = "Luf90449";

        public async Task<ServiceResult<EmailDto>> SendEmailAsync(EmailDto model)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, certificate, chain, sslPolicyErrors) => true;

            using (var client = new SmtpClient(_smtpServer, _port))
            {
                client.Credentials = new System.Net.NetworkCredential(_fromEmail, _password);
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Adana Sanayi Odası"),
                    Subject = model.Subject,
                    Body = model.Body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(model.StaffEmail ?? model.Email);

                try
                {
                    await client.SendMailAsync(mailMessage);
                    return ServiceResult<EmailDto>.Success(new EmailDto(), HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return ServiceResult<EmailDto>.Fail(ex.Message);
                }
            }
        }
    }
}
