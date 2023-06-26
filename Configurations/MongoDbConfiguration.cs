using LicentaApp.Identity;
using LicentaApp.Models;
using LicentaApp.Repositories;
using MongoDB.Driver;

namespace LicentaApp.Configuration
{
    public static class MongoDbConfiguration
    {

        private static IMongoDatabase? _db;
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMongoDb(configuration);
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        public static void ConfigureMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = getMongoDbSettings(configuration);
            _db = CreateMongoDatabase(settings);
            services.addMongoDbRepository<AlbumRepository, AlbumModel>(settings.CollectionName.AlbumCollection);
            services.addMongoDbRepository<ArtistRepository, ArtistModel>(settings.CollectionName.ArtistCollection);
            services.addMongoDbRepository<FeedbackRepository, FeedbackModel>(settings.CollectionName.FeedbackCollection);
            services.addMongoDbRepository<ReviewRepository, ReviewModel>(settings.CollectionName.ReviewCollection);
            services.addMongoDbRepository<UserRepository, UserModel>(settings.CollectionName.UserCollection);
            services.addMongoDbRepository<GenreRepository, GenreModel>(settings.CollectionName.GenreCollection);
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(settings.ConnectionString, settings.DatabaseName);

        }
        private static void addMongoDbRepository<TRepository, TModel>(this IServiceCollection services, string collectionName)
        {
            services.AddSingleton(_db.GetCollection<TModel>(collectionName));
            services.AddSingleton(typeof(TRepository));
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

    //services.addMongoDbRepository<SongRepository, SongModel>(settings.CollectionName.SongCollection);
}
