namespace FastPizza.Service.Dtos.Auth;

public class LoginDto
{
    public string PhoneNumber { get; set; } = String.Empty;

    public int code { get; set; }
}
