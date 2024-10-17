namespace PawFund.Contract.Settings;

public class PayOSSetting
{
    public const string SectionName = "PayOSSetting";
    public string ClientId { get; set; }
    public string ApiKey { get; set; }
    public string ChecksumKey { get; set; }
}
