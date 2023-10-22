namespace WebApi.Models;

using System.ComponentModel.DataAnnotations;

public class UserCredentials
{
    public string login { get; set; }

    public string password { get; set; }
}
