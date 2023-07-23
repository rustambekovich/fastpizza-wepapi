namespace FastPizza.Service.Dtos.Auth;

public class VerifyRegisterByPhoneDto
{
    public string Phone { get; set; } = String.Empty;

    public int Code { get; set; }
}
