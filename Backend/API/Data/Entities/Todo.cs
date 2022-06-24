using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Data.Entities;

public class Todo : IEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ProgressStatus Status { get; set; }

    [Required]
    [JsonIgnore]
    public virtual User User { get; set; }

    [Required]
    [JsonIgnore]
    public virtual IEnumerable<SubTask> SubTask { get; set; }

    public virtual void AddSubTask(SubTask todo)
    {
        SubTask.Append(todo);
    }

    public void AddOwner(User owner)
    {
        User = owner;
    }
}

public enum ProgressStatus
{
    [Display(Name = "COMPLETED")]
    COMPLETED,
    [Display(Name = "STARTED")]
    STARTED,
    [Display(Name = "CREATED")]
    CREATED,
}