using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HitRefresh.MailToMyself
{
    /// <summary>
    /// Options for the mail to send to yourself.
    /// </summary>
    public record LoopbackMailOptions
    {
        /// <summary>
        /// Disable SSL or not.
        /// </summary>
        public bool Unsafe { get; init; }
        /// <summary>
        /// Email address
        /// </summary>
        public string Address { get; init; } = "";
        /// <summary>
        /// SMTP server address
        /// </summary>
        public string Server { get; init; } = "";
        /// <summary>
        /// Port of the smtp Server
        /// </summary>
        public int Port { get; init; } = 587;
        /// <summary>
        /// Parse LoopbackMailOptions from expression.
        /// </summary>
        /// <param name="expr">Format: Server[:Port[U]]:EmailAddress; 'U' stands for unsafe.</param>
        /// <returns></returns>
        public static LoopbackMailOptions Parse(string expr)
        {
            var mailInfo = expr.Split(':');
            if (mailInfo.Length < 2 || mailInfo.Length > 3)
            {
                throw new FormatException($"'{expr}' doesn't satisfy the format Server[:Port[U]]:EmailAddress");
            }
            var addr = mailInfo[^1];
            var server=mailInfo[0];
            if (mailInfo.Length == 3)
            {
                var portInfo = mailInfo[1];
                
                var @unsafe= false;
                if (portInfo.EndsWith('U'))
                {
                    @unsafe= true;
                    portInfo = portInfo[..^1];
                }
                return new()
                {
                    Address = addr,
                    Port = int.Parse(portInfo),
                    Unsafe = @unsafe,
                    Server = server
                };
            }
            return new()
            {
                Address = addr,
                Server = server
            };
        }
    }
}
