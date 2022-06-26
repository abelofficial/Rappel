using System.ComponentModel.DataAnnotations;

namespace API.Data.Entities;

public class Project : IEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }

    [Required]
    public bool IsOrdered { get; set; }

    [Required]
    public virtual User Owner { get; set; }

    [Required]
    public virtual IEnumerable<User> Members { get; set; }

    [Required]
    public virtual IEnumerable<Todo> Items { get; set; }


    public Project()
    {
        Members = new List<User>();
        Items = new List<Todo>();
    }
    public virtual void AddItem(Todo todo)
    {
        Items.Append(todo);
    }

    public void AddMember(User member)
    {
        Members.Append(member);
    }
}
