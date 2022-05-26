using ApplicationFMS.Interfaces;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using MimeKit;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace InfrastructureFMSDB.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        private ApplicationFMS.Models.Message CreateRegistrationMessage(CoreFMS.Entities.User user)
        {
            ApplicationFMS.Models.Message message = ApplicationFMS.Models.Message.CreateRegistrationMessageBase(user);
            string FilePath = Directory.GetCurrentDirectory() + "\\RegistrationMailTemplate.html";
            StreamReader streamReader = new StreamReader(FilePath);
            string MailText = streamReader.ReadToEnd();
            streamReader.Close();
            message.Content = MailText
                .Replace("[username]", user.FirstName + " " + user.LastName)
                .Replace("[email]", user.Email)
                .Replace("[verificationCode]", user.VerificationCode);

            //return CreateEmailMessage(message);
            return message;
        }

        public async Task SendRegistrationMailJetMail(CoreFMS.Entities.User user)
        {
            var mailMessage = CreateRegistrationMessage(user);
            await RunMailJetAsync(mailMessage);
        }

        public async Task RunMailJetAsync(ApplicationFMS.Models.Message message)
        {
            MailjetClient client = new MailjetClient(_emailConfig.ApiKey, _emailConfig.SecretKey);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource
            };

            // construct your email with builder
            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_emailConfig.From))
                .WithSubject(message.Subject)
                .WithHtmlPart(message.Content)
                .WithTo(new SendContact(message.To[0].ToString()))
                .Build();

            // invoke API to send email
            var response = await client.SendTransactionalEmailAsync(email);
        }
    }

}
