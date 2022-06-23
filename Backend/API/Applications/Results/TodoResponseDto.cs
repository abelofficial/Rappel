namespace API.Application.Results;

public class TodoResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public UserResponseDto User { get; set; }
}
