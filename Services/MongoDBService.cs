using MongoDB.Driver;

namespace LicentaApp.Services
{
    public static class MongoDBService
    {

        private static IMongoDatabase? _db;
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureMongoDb(services, configuration);
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = getMongoDbSettings(configuration);
            _db = CreateMongoDatabase(settings);
            services.addMongoDbService<AlbumService, AlbumModel>(settings.CollectionName.AlbumCollection);
            services.addMongoDbService<ArtistService, ArtistModel>(settings.CollectionName.ArtistCollection);
            services.addMongoDbService<FeedbackService, FeedbackModel>(settings.CollectionName.FeedbackCollection);
            services.addMongoDbService<ReviewService, ReviewModel>(settings.CollectionName.ReviewCollection);
            services.addMongoDbService<SongService, SongModel>(settings.CollectionName.SongCollection);
            services.addMongoDbService<UserService, UserModel>(settings.CollectionName.UserCollection);

        }
        static void addMongoDbService<TService, TModel>(this IServiceCollection services, string collectionName)
        {
            services.AddSingleton(_db.GetCollection<TModel>(collectionName));
            services.AddSingleton(typeof(TService));
        }
        private static DatabaseSettingsModel getMongoDbSettings(IConfiguration configuration) =>
            configuration.GetSection(nameof(DatabaseSettingsModel)).Get<DatabaseSettingsModel>();


        private static IMongoDatabase CreateMongoDatabase(DatabaseSettingsModel settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings), "MongoDB settings are null");
            }
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        }
    }
}
