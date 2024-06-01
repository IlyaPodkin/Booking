namespace Администратирование_предприятия.Models;

public class Grade : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Grade(Guid Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}
