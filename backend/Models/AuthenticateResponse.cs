namespace WebApi.Models;

using WebApi.Entities;

public class AuthenticateResponse
{
    public int id { get; set; }
    public string login{ get; set; }
    public bool role { get; set; }
    public DateTime resetDate { get; set; }
    public bool blocked { get; set; }
    public bool criteria { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        id = user.id;
        login = user.login;
        role = user.role;
        resetDate = user.resetDate;
        blocked = user.blocked;
        criteria = user.criteria;
        Token = token;
    }
}
