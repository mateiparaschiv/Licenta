using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ArtistService(IArtistRepository artistService,
            IAlbumRepository albumService,
            IReviewRepository reviewService,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _artistRepository = artistService;
            _albumRepository = albumService;
            _reviewRepository = reviewService;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;

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
            var artist = await _artistRepository.GetArtistByName(name);

            var newReview = new ReviewModel
            {
                Subject = artist.Name,
                Username = null,
                Email = null
            };

            if (_httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = await _userRepository.GetAsync(username);
                newReview.Username = username;
                newReview.Email = user.Email;
            }

            return new IndexArtistNameViewModel
            {
                Artist = artist,
                ArtistAlbums = await _albumRepository.GetFilteredListByArtist(artist.Name, sortOrder),
                SortOrder = sortOrder,
                ReviewList = await _reviewRepository.GetAsyncListByAlbum(name),
                NewReview = newReview
            };
        }
    }
}
