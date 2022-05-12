namespace Common.Config;

#nullable disable

public class RabbitMqConfig
{
    public const string RabbitMq = "RabbitMq";

    public string Host { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public ushort Port { get; set; }
}