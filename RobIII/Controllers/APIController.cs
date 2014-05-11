using RobIII.Helpers;
using RobIII.Models;
using SimpleFeedReader;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace RobIII.Controllers
{
    [RoutePrefix("api")]
    public class APIController : ApiController
    {
        [Route("blogroll/{language}/{page:int?}/{pagesize:int?}")]
        public IQueryable<FeedItem> GetFeeds(string language, int page = 1, int pageSize = 10)
        {
            return this.GetFeeds(language).Skip((page - 1) * pageSize).Take(pageSize);

        }
        public IQueryable<FeedItem> GetFeeds(string language)
        {
            return new FeedRetriever().GetByLanguage(language);
        }

        [Route("contactform")]
        [HttpPost]
        public string[] SendContactMail(ContactformViewmodel model)
        {
            if (!ModelState.IsValid)
                return ModelState.Values.SelectMany(m => m.Errors.Select(e => e.ErrorMessage)).ToArray();

            if (string.IsNullOrEmpty(model.Schd))//Make sure 'spam catch value' is empty
            {
                MailMessage mail = new MailMessage();

                SmtpClient smtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtphost"]);
                smtpServer.Port = int.Parse(ConfigurationManager.AppSettings["smtpport"]);
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["smtpuser"]))
                {
                    smtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpuser"], ConfigurationManager.AppSettings["smtppass"]);
                }

                mail.From = new MailAddress(model.Email, model.Name);
                mail.To.Add(ConfigurationManager.AppSettings["contactform-recipient"]);
                mail.Subject = "Contact-form";
                mail.Body = string.Format("{0}\n----\nIP: {1}\nUA: {2}", model.Message, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserAgent);
                mail.IsBodyHtml = false;

                try
                {
                    smtpServer.Send(mail);
                }
                catch (Exception ex)
                {
                    return new string[] { "Unable to send email. Please try again later", ex.Message };
                }
            }
            return new string[0];
        }
    }
}
