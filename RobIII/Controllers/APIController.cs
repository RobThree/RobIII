using RobIII.Helpers;
using RobIII.Models;
using SimpleFeedReader;
using Dapper;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Collections.Generic;

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

        [Route("pocket/{status}/{page:int?}/{pagesize:int?}")]
        public IEnumerable<PocketItem> GetPocket(string status, int page = 1, int pageSize = 10, string search = null)
        {
            return this.GetPocket(status, search).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<PocketItem> GetPocket(string status, string search = null)
        {
            int? statuscode = null;
            string query = null;

            switch (status.ToLowerInvariant())
            {
                case "read":
                    statuscode = 1;
                    break;
                case "unread":
                    statuscode = 0;
                    break;
                default:
                    statuscode = null;
                    break;
            }

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["riii_db"].ConnectionString))
            {
                conn.Open();

                if (!string.IsNullOrEmpty(search))
                    query = '%' + search.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("_", @"\_").Replace("[", @"\[").Replace("]", @"\]") + '%';

                return conn.Query<PocketItem>(
                    @"SELECT * FROM [dbo].[pocketbookmarks] 
                        Where ((@Status is null) or ([status] = @Status))
                            AND ((@Search is null) or (([title] like @Search ESCAPE '\') or ([fulltitle] like @Search ESCAPE '\') or ([excerpt] like @Search ESCAPE '\')))
                            AND ([isdeleted] = 0)
                        ORDER BY [addtime] desc",
                    new
                    {
                        Status = statuscode,
                        Search = query
                    }
                );
            }
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
                smtpServer.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpusessl"]);
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
