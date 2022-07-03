using API.Domain.Entities;

namespace API.Application.Results;

public class SubTaskResponseDto
{
    public int Id { get; set; }


    public string? Title { get; set; }

    public string? Description { get; set; }

    public ProgressStatus Status { get; set; }

    public int TodoId { get; set; }

    public int ProjectId { get; set; }
}

