namespace Администратирование_предприятия.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    public User(Guid Id, string Login, string Password, string name)
    {
        this.Id = Id;
        this.Login = Login;
        this.Password = Password;
        Name = name;
    }
}
