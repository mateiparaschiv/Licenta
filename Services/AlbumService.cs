using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlbumService(IAlbumRepository albumRepository,
            IReviewRepository reviewRepository,
            IUserRepository userRepository,
            IGenreRepository genreRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _albumRepository = albumRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
            _genreRepository = genreRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(AlbumModel newAlbumModel)
        {
            await _albumRepository.CreateAsync(newAlbumModel);
        }

        public async Task<IndexAlbumListViewModel> IndexAlbumList(string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 9;
            int totalAlbums = await _albumRepository.GetTotalCountAsync();
            int maxPages = (totalAlbums + pageSize - 1) / pageSize;

            return new IndexAlbumListViewModel
            {
                AllAlbumList = await _albumRepository.GetAsyncFilteredByName(),
                AlbumList = await _albumRepository.GetPaginatedFilteredList(sortOrder: sortOrder, pageNumber: pageNumber, pageSize: pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                YearList = await _albumRepository.GetDistinctYearsAsync(),
                GenreList = await _genreRepository.GetDistinctGenresAsync()
            };
        }

        public async Task<IndexAlbumNameViewModel> AlbumName(string albumName, string? returnUrl)
        {
            var album = await _albumRepository.GetAlbumByName(albumName);
            var newReview = new ReviewModel();

            if (UserIsAuthenticated())
            {
                var username = GetCurrentUserName();
                var user = await _userRepository.GetAsync(username);

                if (user == null || user.Email == null)
                {
                    throw new Exception("Authenticated user not found or user's email is not set.");
                }

                newReview.Subject = album.Name;
                newReview.Username = username;
                newReview.Email = user.Email;
                newReview.Title = "";
                newReview.SubjectType = "album";
                newReview.Date = DateTime.Now;
            }

            return new IndexAlbumNameViewModel
            {
                Album = album,
                ReviewList = await _reviewRepository.GetAsyncFilteredByDate(albumName),
                NewReview = UserIsAuthenticated() ? newReview : null,
                ReturnUrl = returnUrl
            };
        }

        public async Task<IndexAlbumYearListViewModel> AlbumsYear(int year, string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 9;
            int totalAlbums = await _albumRepository.GetTotalCountAsync(year: year);
            int maxPages = (totalAlbums + pageSize - 1) / pageSize;

            return new IndexAlbumYearListViewModel
            {
                AlbumList = await _albumRepository.GetPaginatedFilteredList(sortOrder: sortOrder, year: year, pageNumber: pageNumber, pageSize: pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Year = year
            };
        }
        public async Task<IndexAlbumGenreListViewModel> AlbumsGenre(string genre, string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 9;
            int totalAlbums = await _albumRepository.GetTotalCountAsync(genre: genre);
            int maxPages = (totalAlbums + pageSize - 1) / pageSize;

            return new IndexAlbumGenreListViewModel
            {
                AlbumList = await _albumRepository.GetPaginatedFilteredList(sortOrder: sortOrder, genre: genre, pageNumber: pageNumber, pageSize: pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Genre = genre
            };
        }
        public async Task<IndexAlbumSentimentListViewModel> AlbumsSentiment(string sentiment, string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 9;
            int totalAlbums = await _albumRepository.GetTotalCountAsync(sentiment: sentiment);
            int maxPages = (totalAlbums + pageSize - 1) / pageSize;

            return new IndexAlbumSentimentListViewModel
            {
                AlbumList = await _albumRepository.GetPaginatedFilteredList(sortOrder: sortOrder, sentiment: sentiment, pageNumber: pageNumber, pageSize: pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Sentiment = sentiment
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