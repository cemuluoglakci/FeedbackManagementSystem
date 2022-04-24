﻿using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;

namespace InfrastructureFMSDB.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        public async Task SendRegistrationMail(User user)
        {
            MimeMessage mailMessage = CreateRegistrationMessage(user);
            await SendAsync(mailMessage);
        }

        private MimeMessage CreateRegistrationMessage(User user)
        {
            Message message = Message.CreateRegistrationMessageBase(user);
            //string FilePath = Directory.GetCurrentDirectory() + "\\..\\..\\Infrastructure\\InfrastructureFMSDB\\EmailService\\RegistrationMailTemplate.html";
            string FilePath = Directory.GetCurrentDirectory() + "\\RegistrationMailTemplate.html";
            StreamReader streamReader = new StreamReader(FilePath);
            string MailText = streamReader.ReadToEnd();
            streamReader.Close();
            message.Content = MailText
                .Replace("[username]", user.FirstName + " " + user.LastName)
                .Replace("[email]", user.Email)
                .Replace("[verificationCode]", user.VerificationCode);

            return CreateEmailMessage(message);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Name, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private BodyBuilder BuildRegistrationMailBody()
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\..\\..\\Infrastructure\\InfrastructureFMSDB\\EmailService\\RegistrationMailTemplate.html";
            StreamReader streamReader = new StreamReader(FilePath);
            string MailText = streamReader.ReadToEnd();
            streamReader.Close();
            MailText = MailText.Replace("[username]", "John Black").Replace("[email]", "johnblack@gmail.com");

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;

            return builder;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.CheckCertificateRevocation = false;
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, false);
                    //client.AuthenticationMechanisms.Remove("XOAUTH2");
                    //await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}