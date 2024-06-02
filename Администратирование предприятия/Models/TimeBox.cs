namespace Администратирование_предприятия.Models;

public class TimeBox
{
    public Guid Id { get; set; }
    public string? Value { get; set; }
    public string WorkerId { get; set; }
    public string NameWorker { get; set; }

    public TimeBox(Guid Id, string value, string workerId, string nameWorker)
    {
        this.Id = Id;
        this.Value = value;
        WorkerId = workerId;
        NameWorker = nameWorker;
    }
}
