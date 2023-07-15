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
                AllArtistList = await _artistRepository.GetAsyncFilteredByName(),
                ArtistList = artistList,
                AlbumsToArtist = await _albumRepository.GetNumOfAlbumsByNames(artistList),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages
            };
        }

        public async Task<IndexArtistNameViewModel> ArtistName(string name, string sortOrder)
        {
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "desc" : sortOrder;
            var artist = await _artistRepository.GetArtistByName(name);

            var newReview = new ReviewModel();

            if (UserIsAuthenticated())
            {
                var username = GetCurrentUserName();
                var user = await _userRepository.GetAsync(username);

                if (user == null || user.Email == null)
                {
                    throw new Exception("Authenticated user not found or user's email is not set.");
                }

                newReview.Subject = artist.Name;
                newReview.Username = username;
                newReview.Email = user.Email;
                newReview.Title = "";
                newReview.SubjectType = "artist";
                newReview.Date = DateTime.Now;
            }

            return new IndexArtistNameViewModel
            {
                Artist = artist,
                ArtistAlbums = await _albumRepository.GetFilteredListByArtist(artist.Name, sortOrder),
                SortOrder = sortOrder,
                ReviewList = await _reviewRepository.GetAsyncListByAlbum(name),
                NewReview = UserIsAuthenticated() ? newReview : null
            };
        }

        private bool UserIsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        private string GetCurrentUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
        }
    }
}