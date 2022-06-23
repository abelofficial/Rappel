using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    [JsonIgnore]
    [ForeignKey("UserId")]
    public virtual User User { get; set; }

    [Required]
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