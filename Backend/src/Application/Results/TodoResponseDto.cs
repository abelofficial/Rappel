using API.Domain.Entities;

namespace API.Application.Results;

public class TodoResponseDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    public ProgressStatus Status { get; set; }

    public IEnumerable<SubTaskResponseDto>? SubTask { get; set; }

}
