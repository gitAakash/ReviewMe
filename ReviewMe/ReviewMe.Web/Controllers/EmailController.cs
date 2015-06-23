using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ReviewMe.ViewModel;

namespace ReviewMe.Web.Controllers
{
    public class EmailController : Controller
    {
        //
        // GET: /Email/
        string hostAdd = ConfigurationManager.AppSettings["Host"].ToString();
        string fromEmailId = ConfigurationManager.AppSettings["FromEmailId"].ToString();
        string pass = ConfigurationManager.AppSettings["Password"].ToString();
        string port = ConfigurationManager.AppSettings["smtpport"].ToString();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyEmail()
        {
            return View();
        }

        /// <summary>
        /// send Email to reviewer 
        /// </summary>
        /// <param name="objModelMail"></param>
        /// <param name="fileUploader"></param>
        /// <returns></returns>
        /// created by prerna khandelwal
        [HttpPost]
        public ActionResult MyEmail(MyMailViewModal objModelMail, HttpPostedFileBase fileUploader)
        {
            if (ModelState.IsValid)
            {
                string body;
                string from = fromEmailId; //any valid GMail ID
                try
                {
                    using (MailMessage mail = new MailMessage(from, objModelMail.To))
                    {

                        StringBuilder sb = new StringBuilder();
                        sb.Append("Hi Scott,<br/>");
                        sb.Append("This is a test email. We are testing out email client. Please don't mind.<br/>");
                        sb.Append("We are sorry for this unexpected mail to your mail box.<br/>");
                        sb.Append("<br/>");
                        sb.Append("Thanking you<br/>");
                        sb.Append("Tips.Asp.Net");
                        body = sb.ToString();

                        mail.Subject = "Review Me";
                        mail.Body = body;

                        if (fileUploader != null)
                        {
                            string fileName = Path.GetFileName(fileUploader.FileName);
                            mail.Attachments.Add(new Attachment(fileUploader.InputStream, fileName));
                        }

                        mail.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = hostAdd;
                        smtp.EnableSsl = true;
                        NetworkCredential networkCredential = new NetworkCredential(from, pass);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCredential;
                        smtp.Port = Convert.ToInt32(port);

                        smtp.Send(mail);
                        ViewBag.Message = "Sent";
                        return View(objModelMail);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                return View();
            }
        }

	}
}