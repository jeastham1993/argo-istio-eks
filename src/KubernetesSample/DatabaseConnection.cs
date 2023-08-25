namespace DotnetSQLServer.Blueprint;

using System.Text.Json.Serialization;

public record DatabaseConnection()
{
    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}