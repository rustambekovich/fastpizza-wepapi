using FastPizza.Service.Dtos.Notifications;

namespace FastPizza.Service.Interfaces.Notifications;

public interface IPhoneSender
{
    public Task<bool> SenderAsync(PhoneMessage phonemessage);

}
