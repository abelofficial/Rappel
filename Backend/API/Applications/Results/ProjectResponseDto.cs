namespace API.Application.Results;

public class ProjectResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public bool IsOrdered { get; set; }

    public UserResponseDto Owner { get; set; }

    public IEnumerable<UserResponseDto> Members { get; set; }

    public IEnumerable<TodoResponseDto> Items { get; set; }

}
