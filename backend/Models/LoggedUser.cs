namespace WebApi.Models;

public class LoggedUser
{
    public int id { get; set; }
    public string Token { get; set; }

    public LoggedUser(int id, string token)
    {
        this.id = id;
        this.Token = token;
    }
}
