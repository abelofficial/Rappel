namespace API.Application.Results;

public class SubTaskResponseDto
{
    public int Id { get; set; }


    public string Title { get; set; }

    public string Description { get; set; }

    public TodoResponseDto Todo { get; set; }
}

