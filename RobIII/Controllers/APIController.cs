using Dapper;
using RobIII.Helpers;
using RobIII.Models;
using SimpleFeedReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            => GetFeeds(language).Skip((page - 1) * pageSize).Take(pageSize);
        public IQueryable<FeedItem> GetFeeds(string language)
            => new FeedRetriever().GetByLanguage(language);

        [Route("pocket/{status}/{page:int?}/{pagesize:int?}")]
        public IEnumerable<PocketItem> GetPocket(string status, int page = 1, int pageSize = 10, string search = null)
            => GetPocket(status, search).Skip((page - 1) * pageSize).Take(pageSize);

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
                {
                    query = '%' + search.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("_", @"\_").Replace("[", @"\[").Replace("]", @"\]") + '%';
                }

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
            {
                return ModelState.Values.SelectMany(m => m.Errors.Select(e => e.ErrorMessage)).ToArray();
            }

            if (string.IsNullOrEmpty(model.Schd))//Make sure 'spam catch value' is empty
            {
                var mail = new MailMessage();

                var smtpServer = new SmtpClient(ConfigurationManager.AppSettings["smtphost"])
                {
                    Port = int.Parse(ConfigurationManager.AppSettings["smtpport"]),
                    EnableSsl = bool.Parse(ConfigurationManager.AppSettings["smtpusessl"])
                };
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["smtpuser"]))
                {
                    smtpServer.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpuser"], ConfigurationManager.AppSettings["smtppass"]);
                }

                //return HttpContext.Current.Request.Headers.AllKeys.Select(k=> k + ": " + HttpContext.Current.Request.Headers[k]).ToArray();
                var ip = string.IsNullOrEmpty(HttpContext.Current.Request.Headers["X-Forwarded-For"])
                    ? HttpContext.Current.Request.UserHostAddress
                    : HttpContext.Current.Request.Headers["X-Forwarded-For"];
                mail.ReplyToList.Add(new MailAddress(model.Email, model.Name));
                mail.From = new MailAddress(ConfigurationManager.AppSettings["contactform-recipient"], ConfigurationManager.AppSettings["contactform-recipient-name"]);
                mail.To.Add(ConfigurationManager.AppSettings["contactform-recipient"]);
                mail.Subject = "Contact-form";
                mail.Body = string.Format("{0}\n----\nIP: {1}\nUA: {2}", model.Message, ip, HttpContext.Current.Request.UserAgent);
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
