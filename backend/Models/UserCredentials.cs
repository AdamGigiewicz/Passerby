namespace WebApi.Models;

using System.ComponentModel.DataAnnotations;

public class UserCredentials
{
    [Required]
    public string login { get; set; }

    [Required]
    public string password { get; set; }
}
