using LicentaApp.Models;
using LicentaApp.Models.ViewModels.ArtistViewModel;

namespace LicentaApp.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserService _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ArtistService(IArtistRepository artistService,
            IAlbumRepository albumService,
            IReviewRepository reviewService,
            IUserService userRepository,
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
            const int pageSize = 10;
            int totalArtists = await _artistRepository.GetTotalCountAsync();
            int maxPages = (totalArtists + pageSize - 1) / pageSize;

            List<ArtistModel> artistList = await _artistRepository.GetPaginatedFilteredList(sortOrder: sortOrder, pageNumber: pageNumber, pageSize: pageSize);

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
        public async Task<IndexArtistSentimentListViewModel> ArtistsSentiment(string sentiment, string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 10;
            int totalArtists = await _artistRepository.GetTotalCountAsync(sentiment: sentiment);
            int maxPages = (totalArtists + pageSize - 1) / pageSize;

            List<ArtistModel> artistList = await _artistRepository.GetPaginatedFilteredList(sentiment: sentiment, sortOrder: sortOrder, pageNumber: pageNumber, pageSize: pageSize);

            return new IndexArtistSentimentListViewModel
            {
                ArtistList = artistList,
                AlbumsToArtist = await _albumRepository.GetNumOfAlbumsByNames(artistList),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Sentiment = sentiment
            };
        }
        public async Task<IndexArtistFormationListViewModel> ArtistsFormation(bool band, string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 10;
            int totalArtists = await _artistRepository.GetTotalCountAsync(band: band);
            int maxPages = (totalArtists + pageSize - 1) / pageSize;

            List<ArtistModel> artistList = await _artistRepository.GetPaginatedFilteredList(band: band, sortOrder: sortOrder, pageNumber: pageNumber, pageSize: pageSize);

            return new IndexArtistFormationListViewModel
            {
                ArtistList = artistList,
                AlbumsToArtist = await _albumRepository.GetNumOfAlbumsByNames(artistList),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Band = band
            };
        }

        public async Task<IndexArtistNameViewModel> ArtistsName(string name, string sortOrder)
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
                ArtistBestAlbums = await _albumRepository.GetFilteredListByCompoundScore(albumArtist: artist.Name),
                SortOrder = sortOrder,
                ReviewList = await _reviewRepository.GetAsyncFilteredByDate(name),
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