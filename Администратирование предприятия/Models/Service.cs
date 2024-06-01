namespace Администратирование_предприятия.Models;

public class Service : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string GradeName { get; set; }
    public string GradeId { get; set; }
    public int? Price { get; set; }
    public string? Time { get; set; }

    public Service(Guid Id, string? Name, string GradeName, string GradeId, int? Price, string Time)
    {
        this.Id = Id;
        this.Name = Name;
        this.GradeName = GradeName;
        this.GradeId = GradeId;
        this.Price = Price;
        this.Time = Time;
    }
}
