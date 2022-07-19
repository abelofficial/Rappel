namespace API.Application.Results;

public class LoginResponseDto
{

    public string Token { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
