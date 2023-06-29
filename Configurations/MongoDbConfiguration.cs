using LicentaApp.Identity;
using LicentaApp.Models;
using LicentaApp.Repositories;
using MongoDB.Driver;

namespace LicentaApp.Configuration
{
    public static class MongoDBConfiguration
    {

        private static IMongoDatabase? _db;
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMongoDB(configuration);
            services.AddControllers().AddNewtonsoftJson(options => options.UseMemberCasing());
        }

        public static void ConfigureMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = getMongoDBSettings(configuration);
            _db = CreateMongoDatabase(settings);
            services.addMongoDBRepository<AlbumRepository, AlbumModel>(settings.CollectionName.AlbumCollection);
            services.addMongoDBRepository<ArtistRepository, ArtistModel>(settings.CollectionName.ArtistCollection);
            services.addMongoDBRepository<FeedbackRepository, FeedbackModel>(settings.CollectionName.FeedbackCollection);
            services.addMongoDBRepository<ReviewRepository, ReviewModel>(settings.CollectionName.ReviewCollection);
            services.addMongoDBRepository<UserRepository, UserModel>(settings.CollectionName.UserCollection);
            services.addMongoDBRepository<GenreRepository, GenreModel>(settings.CollectionName.GenreCollection);
            services.addMongoDBRepository<SongRepository, SongModel>(settings.CollectionName.SongCollection);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>(settings.ConnectionString, settings.DatabaseName);

        }
        private static void addMongoDBRepository<TRepository, TModel>(this IServiceCollection services, string collectionName)
        {
            services.AddSingleton(_db.GetCollection<TModel>(collectionName));
            services.AddSingleton(typeof(TRepository));
        }
        private static DatabaseSettingsModel getMongoDBSettings(IConfiguration configuration) =>
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
