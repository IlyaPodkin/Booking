namespace Администратирование_предприятия.Models;

public class Grade
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public float Coefficient { get; set; }

    public Grade(Guid Id, string Name, float coefficient)
    {
        this.Id = Id;
        this.Name = Name;
        this.Coefficient = coefficient;
    }
}
