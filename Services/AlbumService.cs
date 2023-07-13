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
                AlbumList = await _albumRepository.GetPaginatedFilteredList(sortOrder, pageNumber, pageSize),
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                MaxPages = maxPages
            };
        }

        public async Task<IndexAlbumNameViewModel> AlbumName(string name)
        {
            var album = await _albumRepository.GetAlbumByName(name);
            var reviewList = await _reviewRepository.GetAsyncListByAlbum(name);

            var newReview = new ReviewModel
            {
                Subject = album.Name,
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

            return new IndexAlbumNameViewModel
            {
                Album = album,
                ReviewList = reviewList,
                NewReview = newReview
            };
        }
    }
}
