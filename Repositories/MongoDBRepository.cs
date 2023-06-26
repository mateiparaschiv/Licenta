using LicentaApp.Identity;
using LicentaApp.Models;
using MongoDB.Driver;

namespace LicentaApp.Repositories
{
    public static class MongoDbRepository
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
            services.addMongoDbService<AlbumRepository, AlbumModel>(settings.CollectionName.AlbumCollection);
            services.addMongoDbService<ArtistRepository, ArtistModel>(settings.CollectionName.ArtistCollection);
            services.addMongoDbService<FeedbackRepository, FeedbackModel>(settings.CollectionName.FeedbackCollection);
            services.addMongoDbService<ReviewRepository, ReviewModel>(settings.CollectionName.ReviewCollection);
            services.addMongoDbService<SongRepository, SongModel>(settings.CollectionName.SongCollection);
            services.addMongoDbService<UserRepository, UserModel>(settings.CollectionName.UserCollection);
            services.addMongoDbService<GenreRepository, GenreModel>(settings.CollectionName.GenreCollection);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(settings.ConnectionString, settings.DatabaseName);

        }
        private static void addMongoDbService<TService, TModel>(this IServiceCollection services, string collectionName)
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
