using System;
using System.Net.Mail;

namespace ReviewMe.Common.Helpers
{
    public class EmailHelper
    {
        public static bool SendMail(string toEmailAddress, string subject, string messageBody)
        {
            try
            {
                var smtpClient = new SmtpClient();
                var message = new MailMessage();

                message.To.Add(toEmailAddress);
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = messageBody;

                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}