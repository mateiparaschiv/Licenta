namespace LicentaApp.Repositories
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IAlbumService _albumService;
        private readonly IReviewService _reviewService;
        public AlbumRepository(IAlbumService albumService, IReviewService reviewService)
        {
            _albumService = albumService;
            _reviewService = reviewService;
        }
        public async Task<Tuple<List<AlbumModel>, string>> IndexAlbumList(string? name, string? sortOrder)
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
            var tuple = new Tuple<List<AlbumModel>, string>(albumList, sortOrder);
            return tuple;
        }

        public async Task<Tuple<AlbumModel, List<ReviewModel>>> IndexAlbumName(string? name)
        {
            var album = await _albumService.GetAsyncByName(name);
            var review = await _reviewService.GetAsyncListByAlbum(name);
            var tuple = new Tuple<AlbumModel, List<ReviewModel>>(album, review);
            return tuple;
        }
    }
}
