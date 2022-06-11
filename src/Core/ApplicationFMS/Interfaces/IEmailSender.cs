using ApplicationFMS.Models;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Interfaces
{
    public interface IEmailSender
    {
        //void SendEmail(Message message);
        //Task SendEmailAsync(Message message);
        //Task SendRegistrationMail(User user);
        Task SendRegistrationMailJetMail(User user);
    }
}
