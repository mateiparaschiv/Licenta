using LicentaApp.Models.ViewModels;

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

        public async Task<IndexArtistListViewModel> IndexArtistList(string? name, string? sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
            var artistList2 = await _artistService.GetAsync();
            var perPage = 9;
            var artistList = await _artistService.GetPaginatedListAsync(perPage, pageNumber);
            var maxPages = artistList2.Count() / perPage;
            //_artistService.Shuffle(artistList);
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
            IndexArtistListViewModel indexArtistListViewModel = new IndexArtistListViewModel
            {
                ArtistList = artistList,
                AlbumsToArtist = albumsToArtist,
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages
            };
            return indexArtistListViewModel;
        }

        public async Task<IndexArtistNameViewModel> IndexArtistName(string? name, string? sortOrder)
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
            IndexArtistNameViewModel indexArtistNameViewModel = new IndexArtistNameViewModel
            {
                Artist = artist,
                ArtistAlbums = artistAlbums,
                SortOrder = sortOrder,
                Reviews = reviews
            };
            return indexArtistNameViewModel;
        }
    }
}
