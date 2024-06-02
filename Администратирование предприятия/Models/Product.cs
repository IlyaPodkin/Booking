namespace Администратирование_предприятия.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Info { get; set; }

    public Product(Guid id, string name, int price, string info) 
    {
        this.Id = id;
        this.Name = name;   
        this.Price = price; 
        this.Info = info;
    }
}
