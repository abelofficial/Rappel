using System.ComponentModel.DataAnnotations;

namespace API.Domain.Entities;

public class SubTask : IEntity
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public Todo? Todo { get; set; }

    [Required]
    public ProgressStatus Status { get; set; }
}