namespace arviews_service.API.Models.Trendlog
{
    public class TrendlogServiceSettings : ITrendlogServiceSettings
    {
        public string ApiKey { get; set; }
    }

    public interface ITrendlogServiceSettings
    {
        string ApiKey { get; set; }
    }
}
