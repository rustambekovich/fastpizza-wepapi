namespace FastPizza.Service.Dtos.Notifications;

public class PhoneMessage
{
    public string Recipent { get; set; } = String.Empty;

    public string Title { get; set; } = String.Empty;

    public string Content { get; set; } = String.Empty;
}
