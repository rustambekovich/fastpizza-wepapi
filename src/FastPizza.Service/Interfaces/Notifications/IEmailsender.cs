using FastPizza.Service.Dtos.Notifications;

namespace FastPizza.Service.Interfaces.Notifications
{
    public interface IEmailsender
    {
        public Task<bool> SenderAsync(EmailMessage emailMessage);
    }
}
