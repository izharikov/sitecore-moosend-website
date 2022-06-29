namespace Foundation.Moosend.Models
{
    public interface IMoosendSettings
    {
        string Format { get; }
        string ApiKey { get; }
    }

    public class MoosendSettings: IMoosendSettings
    {
        public string Format { get; set; } = "json";
        public string ApiKey { get; set; }
    }
}