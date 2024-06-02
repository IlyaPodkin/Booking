namespace Администратирование_предприятия.Models;

public class Service
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string GradeName { get; set; }
    public string GradeId { get; set; }
    public float Coefficient { get; set; }
    public float? Price { get; set; }
    public string? Time { get; set; }

    public Service(Guid Id, string? Name, string GradeName, string GradeId, float coefficient, float? Price, string Time)
    {
        this.Id = Id;
        this.Name = Name;
        this.GradeName = GradeName;
        this.GradeId = GradeId;
        this.Coefficient = coefficient;
        this.Price = Price;
        this.Time = Time;
    }
}
