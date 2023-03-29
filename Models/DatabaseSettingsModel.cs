namespace LicentaApp.Models
{
    public class DatabaseSettingsModel
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        //public string CollectionName { get; set; } = null!;
        public class CollectionName
        {
            public string name { get; set; } = null!;
        }

    }
}
