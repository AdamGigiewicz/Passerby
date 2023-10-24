namespace WebApi.Models;

public class LoggedUser
{
    public string Token { get; set; }

    public LoggedUser(string token)
    {
        this.Token = token;
    }
}
