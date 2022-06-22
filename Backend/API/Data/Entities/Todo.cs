using System.ComponentModel.DataAnnotations;

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
    public IEnumerable<Todo> SubTodoList { get; set; } = new List<Todo>() { };

    public DateTime CreatedAt { get; set; } = DateTime.Now;


    public void AddSubTodo(Todo todo)
    {
        if (todo.Id == Id) throw new ArgumentException("A todo can not be its own sub todo.");

        if (todo.SubTodoList.Count() != 0) throw new ArgumentException("A parent todo can not be a subtask.");

        SubTodoList.Append(todo);
    }


}