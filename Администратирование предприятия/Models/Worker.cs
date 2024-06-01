namespace Администратирование_предприятия.Models;

public class Worker
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string GradeId { get; set; }
    public string GradeName { get; set; }
    public string? Info { get; set; }

    public Worker(Guid Id, string Name, string GradeId, string GradeName, string Info)
    {
        this.Id = Id;
        this.Name = Name;
        this.GradeId = GradeId;
        this.GradeName = GradeName;
        this.Info = Info;
    }
}
