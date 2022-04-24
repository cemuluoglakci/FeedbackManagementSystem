using CoreFMS.Entities;
using Microsoft.AspNetCore.Http;
using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationFMS.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public User? NewUser { get; set; }

        public IFormFileCollection? Attachments { get; set; }


        public Message(IEnumerable<string> to, string subject, string content, IFormFileCollection attachments)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x, x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }

        public Message(User user, string subject, string content)
        {
            To = new List<MailboxAddress>()
            {
                new MailboxAddress(user.FirstName, user.Email)
            };
            Subject = subject;
            Content = content;
        }

        public static Message CreateRegistrationMessageBase(User user)
        {
            string subject = "Registiring FMS";
            return new Message(user, subject, "");
        }

    }
}
