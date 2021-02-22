namespace arviews_service.API.Models
{
    public class ArViewsServiceDatabaseSettings : IArViewsServiceDatabaseSettings
    {
        public string ArConfigsCollectionName { get; set; }
        public string WorkspacesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IArViewsServiceDatabaseSettings
    {
        string ArConfigsCollectionName { get; set; }
        public string WorkspacesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
