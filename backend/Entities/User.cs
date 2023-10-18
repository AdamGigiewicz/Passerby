namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int id { get; set; }
    public string login { get; set; }
    public bool isAdmin { get; set; }
    public bool isBlocked { get; set; }
    public bool passwordCriteria { get; set; }
    public DateTime passwordResetDate { get; set; }

    [JsonIgnore]
    public string password { get; set; }
}
