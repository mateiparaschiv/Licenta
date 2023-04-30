using LicentaApp.Interfaces.IRepository;
using LicentaApp.Interfaces.IService;

namespace LicentaApp.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IArtistService _artistService;
        private readonly IAlbumService _albumService;
        private readonly IReviewService _reviewService;
        public ArtistRepository(IArtistService artistService, IAlbumService albumService, IReviewService reviewService)
        {
            _artistService = artistService;
            _albumService = albumService;
            _reviewService = reviewService;
        }

        public async Task<Tuple<PaginatedListModel<ArtistModel>, Dictionary<string, int>, string>> IndexArtistList(string? name, string? sortOrder, int? pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
            int pageSize = 9;
            var artistList = await _artistService.GetAsync();
            //.AsNoTracking()
            var paginatedArtistList = await PaginatedListModel<ArtistModel>.CreateAsync(artistList, pageNumber ?? 1, pageSize);//TODO : PAGINATION
            _artistService.Shuffle(artistList);
            switch (sortOrder)
            {
                case "name_asc":
                    artistList.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case "name_desc":
                    artistList.Sort((x, y) => string.Compare(y.Name, x.Name));
                    break;
                case "":
                    break;
            }

            var albumsToArtist = await _albumService.GetNumOfAlbumsByNames(artistList);
            var tuple = new Tuple<PaginatedListModel<ArtistModel>, Dictionary<string, int>, string>(paginatedArtistList, albumsToArtist, sortOrder);
            return tuple;
        }

        public async Task<Tuple<ArtistModel, List<AlbumModel>, string, List<ReviewModel>>> IndexArtistName(string? name, string? sortOrder)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
            var artist = await _artistService.GetAsyncByName(name);
            var artistAlbums = await _albumService.GetAsyncListByName(name);
            var reviews = await _reviewService.GetAsyncListByAlbum(name);
            artistAlbums.Sort((x, y) => y.Year - x.Year);

            switch (sortOrder)
            {
                case "year_asc":
                    artistAlbums.Sort((x, y) => x.Year - y.Year);
                    break;
                case "year_desc":
                    artistAlbums.Sort((x, y) => y.Year - x.Year);
                    break;
                case "":
                    break;
            }
            var tuple = new Tuple<ArtistModel, List<AlbumModel>, string, List<ReviewModel>>(artist, artistAlbums, sortOrder, reviews);
            return tuple;
        }
    }
}
