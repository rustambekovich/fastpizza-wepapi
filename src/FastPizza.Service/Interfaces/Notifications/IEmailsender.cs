using FastPizza.Service.Dtos.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Interfaces.Notifications
{
    public interface IEmailsender
    {
        public Task<bool> SenderAsync(EmailMessage emailMessage);
    }
}
