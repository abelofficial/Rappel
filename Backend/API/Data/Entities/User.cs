using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Data.Entities;

public class User : IEntity
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    public string FullName { get => FirstName + " " + LastName; }
    [JsonIgnore]
    public byte[] PasswordHash { get; set; }
    [JsonIgnore]
    public byte[] PasswordSalt { get; set; }

    public virtual IEnumerable<Todo> TodoItems { get; set; }

    public DateTime CreatedAt { get; set; }
}