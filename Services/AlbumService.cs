using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AlbumService(IAlbumRepository albumRepository,
            IReviewRepository reviewRepository,
            IUserRepository userRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _albumRepository = albumRepository;
            _reviewRepository = reviewRepository;
            _userRepository = userRepository;
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
                MaxPages = maxPages
            };
        }

        public async Task<IndexAlbumNameViewModel> AlbumName(string albumName)
        {
            var album = await _albumRepository.GetAlbumByName(albumName);
            var reviewList = await _reviewRepository.GetAsyncFilteredByDate(albumName);

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
                ReviewList = reviewList,
                NewReview = UserIsAuthenticated() ? newReview : null
            };
        }

        public async Task<IndexAlbumYearListViewModel> AlbumsYear(int year, string sortOrder, int pageNumber)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            const int pageSize = 9;
            int totalAlbums = await _albumRepository.GetTotalCountAsync(year);
            int maxPages = (totalAlbums + pageSize - 1) / pageSize;

            return new IndexAlbumYearListViewModel
            {
                AlbumList = await _albumRepository.GetPaginatedFilteredList(sortOrder, year, pageNumber, pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages,
                Year = year
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