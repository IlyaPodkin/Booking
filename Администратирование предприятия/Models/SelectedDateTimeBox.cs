namespace Администратирование_предприятия.Models;

public class SelectedDateTimeBox
{
    public Guid Id { get; set; }
    public string? Time { get; set; }
    public string? Date { get; set; }
    public string WorkerId { get; set; }
    public string TimeBoxesId { get; set; }

    public SelectedDateTimeBox(Guid Id, string time, string date, string workerId, string timeBoxesId)
    {
        this.Id = Id;
        this.Time = time;
        this.Date = date;
        WorkerId = workerId;
        TimeBoxesId = timeBoxesId;
    }
}
