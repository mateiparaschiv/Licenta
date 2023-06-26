using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IReviewRepository _reviewRepository;
        public ArtistService(IArtistRepository artistService, IAlbumRepository albumService, IReviewRepository reviewService)
        {
            _artistRepository = artistService;
            _albumRepository = albumService;
            _reviewRepository = reviewService;
        }

        public async Task<IndexArtistListViewModel> IndexArtistList(string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 9;
            int totalArtists = await _artistRepository.GetTotalCountAsync();
            int maxPages = (totalArtists + pageSize - 1) / pageSize;

            List<ArtistModel> artistList = await _artistRepository.GetPaginatedFilteredList(sortOrder, pageNumber, pageSize);

            return new IndexArtistListViewModel
            {
                ArtistList = artistList,
                AlbumsToArtist = await _albumRepository.GetNumOfAlbumsByNames(artistList),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages
            };
        }
        public async Task<IndexArtistNameViewModel> ArtistName(string name, string sortOrder)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            var artistName = (await _artistRepository.GetAsyncByName(name)).Name;
            return new IndexArtistNameViewModel
            {
                Artist = await _artistRepository.GetAsyncByName(name),
                ArtistAlbums = await _albumRepository.GetFilteredListByArtist(artistName, sortOrder),
                SortOrder = sortOrder,
                Reviews = await _reviewRepository.GetAsyncListByAlbum(name)
            };
        }
    }
}
