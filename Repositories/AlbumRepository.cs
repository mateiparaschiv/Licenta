using LicentaApp.Models;
using LicentaApp.Models.ViewModels;

namespace LicentaApp.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IAlbumService _albumService;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AlbumRepository(IAlbumService albumService, IReviewService reviewService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _albumService = albumService;
            _reviewService = reviewService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task CreateAsync(AlbumModel newAlbumModel)
        {
            _albumService.CreateAsync(newAlbumModel);
        }

        public async Task<IndexAlbumListViewModel> IndexAlbumList(string? sortOrder)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;
            List<AlbumModel> albumList;
            switch (sortOrder)
            {
                case "desc":
                    albumList = await _albumService.GetAsyncListDescending();
                    break;
                default: // default is ascending
                    albumList = await _albumService.GetAsyncListAscending();
                    break;
            }
            IndexAlbumListViewModel indexAlbumListViewModel = new IndexAlbumListViewModel
            {
                AlbumList = albumList,
                SortOrder = sortOrder
            };
            return indexAlbumListViewModel;
        }

        public async Task<IndexAlbumNameViewModel> AlbumName(string name)
        {
            var album = await _albumService.GetAsyncByName(name);
            var reviewList = await _reviewService.GetAsyncListByAlbum(name);
            var newReview = new ReviewModel
            {
                Subject = album.Name,
                Username = null,
                Email = null
            };
            if (_httpContextAccessor.HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                var username = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = await _userService.GetAsync(username);
                newReview.Username = username;
                newReview.Email = user.Email;
            }

            IndexAlbumNameViewModel indexAlbumNameViewModel = new IndexAlbumNameViewModel
            {
                Album = album,
                ReviewList = reviewList,
                NewReview = newReview
            };
            return indexAlbumNameViewModel;
        }
    }
}
