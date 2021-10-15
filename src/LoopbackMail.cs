using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HitRefresh.MailToMyself
{
    /// <summary>
    /// A mail to send to yourself.
    /// </summary>
    public class LoopbackMail
    {
        private readonly SmtpClient _smtp;
        private readonly string _account;
        /// <summary>
        /// Initialize a loopback mail with specific options.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="password"></param>
        public LoopbackMail(LoopbackMailOptions options, string password)
        {
            _account = options.Address;
            _smtp = new SmtpClient()
            {
                Port = options.Port,
                UseDefaultCredentials = false,
                EnableSsl = !options.Unsafe,
                Host = options.Server,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(_account, password)
            };

        }
        /// <summary>
        /// Send Loopback Mail with given subject, body and botName.
        /// </summary>
        /// <param name="subject">Mail subject.</param>
        /// <param name="body">Mail body</param>
        /// <param name="botName">name of sending bot.</param>
        /// <returns></returns>
        public async Task SendAsync(string subject,string body,string botName="BOT")
        {
            await _smtp.SendMailAsync(
            new MailMessage(new MailAddress(_account, botName), new MailAddress(_account))
            {
                Body = body,
                Subject = subject,
            });

        }
    }
}
