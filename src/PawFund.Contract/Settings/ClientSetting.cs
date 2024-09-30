namespace PawFund.Contract.Settings;

public class ClientSetting
{
    public const string SectionName = "RedisConfiguration";
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; }
}

