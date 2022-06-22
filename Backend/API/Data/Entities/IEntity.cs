namespace API.Data.Entities;

public interface IEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; protected set; }

}