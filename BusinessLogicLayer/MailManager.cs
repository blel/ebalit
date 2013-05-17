using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace EbalitWebForms.BusinessLogicLayer
{
    /// <summary>
    /// Singleton MailManager to send comments to a predefined receiver.
    ///
    /// </summary>
    public class MailManager
    {
        /// <summary>
        /// The Mail manager instance
        /// </summary>
        private static MailManager _instance;

        private const string _to = "elias.balmer@ebalit.ch";
        private const string _from = _to;
        private SmtpClient _smtpClient;

        /// <summary>
        /// private constructor
        /// </summary>
        private MailManager()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Host = "smtp.ebalit.ch";
            _smtpClient.Credentials = (System.Net.ICredentialsByHost)new System.Net.NetworkCredential(_from, "Chilly13!");
        }

        /// <summary>
        /// The singleton getInstance method for the mail manager
        /// </summary>
        /// <returns></returns>
        public static MailManager GetMailManager()
        {
            if (_instance == null)
                _instance = new MailManager();
            return _instance;
        }

        /// <summary>
        /// send the comment
        /// </summary>
        /// <param name="comment"></param>
        public void SendCommentMessage(DataLayer.BlogComment comment)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_from);
                mailMessage.To.Add(new MailAddress(_to));
                mailMessage.Subject = "New comment for blog entry subject: " + comment.BlogEntry.Subject;
                string mailContent = "The user \"" + comment.UserName + "\" with e-mail address \"" + comment.eMail + "\" has posted a comment for the above mentioned entry.\r\n";
                mailContent += "Subject: \"" + comment.Subject + "\"\r\n";
                mailContent += "Content: \"" + comment.Content + "\"\r\n";
                mailContent += "Posted on " + comment.PostedOn.ToShortDateString() + "\r\n";
                mailMessage.Body = mailContent;

                _smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                //we should use a better logger ....
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

        }
    }
}