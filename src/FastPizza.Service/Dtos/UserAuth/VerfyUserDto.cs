namespace FastPizza.Service.Dtos.UserAuth;

public class VerfyUserDto
{
    public string PhoneNumber { get; set; } = String.Empty;

    public int Code { get; set; }
}
