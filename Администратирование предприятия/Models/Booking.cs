namespace Администратирование_предприятия.Models;

public class Booking
{
    public Guid Id { get; set; }
    public string NameClient { get; set; }
    public string NumberPhone { get; set; }
    public string NameWorker { get; set; }
    public string NameService { get; set; }
    public string? NameProduct { get; set; }
    public int Price { get; set; }
    public string WorkerId { get; set; }
    public string ServiceId { get; set; }
    public string? ProductId { get; set; }
    public string SelectedDateTimeBoxesId { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public string Info { get; set; }
    public string IsChecked { get; set; }

    public Booking(Guid Id, string NameClient, string NumberPhone, string NameWorker, string NameService, string NameProduct, string WorkerId, string ServiceId, string ProductId, string selectedDateTimeBoxesId, int Price, string Date, string Time, string Info, string IsChecked)
    {
        this.Id = Id; 
        this.NameClient = NameClient;
        this.NumberPhone = NumberPhone;
        this.NameWorker = NameWorker;
        this.NameService = NameService;
        this.NameProduct = NameProduct;
        this.Price = Price;
        this.WorkerId = WorkerId;
        this.ServiceId = ServiceId;
        this.ProductId = ProductId;
        this.SelectedDateTimeBoxesId = selectedDateTimeBoxesId;
        this.Date = Date;
        this.Time = Time;
        this.Info = Info;
        this.IsChecked = IsChecked;
    }
}
