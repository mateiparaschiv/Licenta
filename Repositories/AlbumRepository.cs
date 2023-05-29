﻿using LicentaApp.Models;
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
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "" : sortOrder;
            var albumList = await _albumService.GetAsync();
            _albumService.Shuffle(albumList);
            switch (sortOrder)
            {
                case "name_asc":
                    albumList.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case "name_desc":
                    albumList.Sort((x, y) => string.Compare(y.Name, x.Name));
                    break;
                case "":
                    break;
            }
            IndexAlbumListViewModel indexAlbumListViewModel = new IndexAlbumListViewModel
            {
                AlbumList = albumList,
                SortOrder = sortOrder
            };
            //var tuple = new Tuple<List<AlbumModel>, string>(albumList, sortOrder);
            return indexAlbumListViewModel;
        }

        public async Task<IndexAlbumNameViewModel> IndexAlbumName(string name)
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
