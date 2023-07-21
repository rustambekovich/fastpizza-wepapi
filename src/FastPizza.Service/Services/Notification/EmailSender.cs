using FastPizza.Service.Dtos.Notifications;
using FastPizza.Service.Interfaces.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Services.Notification
{
    public class EmailSender : IEmailsender
    {
        public Task<bool> SenderAsync(EmailMessage emailMessage)
        {
            throw new NotImplementedException();
        }
    }
}
