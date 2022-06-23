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
    public IEnumerable<Todo> SubTodoList { get; set; } = new List<Todo>() { };

    public void AddSubTodo(Todo todo)
    {
        if (todo.Id == Id) throw new ArgumentException("A todo can not be its own sub todo.");

        if (todo.SubTodoList.Count() != 0) throw new ArgumentException("A parent todo can not be a subtask.");

        SubTodoList.Append(todo);
    }

    public void AddOwner(User owner)
    {
        User = owner;
    }

}