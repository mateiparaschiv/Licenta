using LicentaApp.Models;
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

        public async Task<IndexArtistListViewModel> IndexArtistList(string? sortOrder, int pageNumber)
        {
            List<ArtistModel> artistList;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int PageSize = 10;
            int totalArtists = await _artistService.GetTotalCountAsync();
            int maxPages = (totalArtists + PageSize - 1) / PageSize;

            switch (sortOrder)
            {
                case "desc":
                    artistList = await _artistService.GetPaginatedListAsyncDescending(pageNumber, PageSize);
                    break;
                default: // default is ascending
                    artistList = await _artistService.GetPaginatedListAsyncAscending(pageNumber, PageSize);
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

        public async Task<IndexArtistNameViewModel> ArtistName(string name, string? sortOrder)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            var artist = await _artistService.GetAsyncByName(name);
            List<AlbumModel> artistAlbums;
            var reviews = await _reviewService.GetAsyncListByAlbum(name);

            switch (sortOrder)
            {
                case "desc":
                    artistAlbums = await _albumService.GetAsyncListByYearDescending(name);
                    break;
                default: // default is ascending
                    artistAlbums = await _albumService.GetAsyncListByYearAscending(name);
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
