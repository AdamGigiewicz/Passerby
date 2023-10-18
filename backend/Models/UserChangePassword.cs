namespace WebApi.Models;

using System.ComponentModel.DataAnnotations;

public class UserChangePassword
{
    [Required]
    public int id { get; set; }

    [Required]
    public string oldPassword { get; set; }

    [Required]
    public string newPassword { get; set; }
}
