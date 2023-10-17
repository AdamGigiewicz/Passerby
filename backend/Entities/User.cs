namespace WebApi.Entities;

using System.Text.Json.Serialization;

public class User
{
    public int id { get; set; }
    public string login { get; set; }
    public string role { get; set; }
    public DateTime resetDate { get; set; }
    public bool blocked { get; set; }
    public bool criteria { get; set; }

    [JsonIgnore]
    public string password { get; set; }
    [JsonIgnore]
    public byte[] salt { get; set; }
}
