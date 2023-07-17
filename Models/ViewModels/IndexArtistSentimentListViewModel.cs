﻿namespace LicentaApp.Models.ViewModels
{
    public class IndexArtistSentimentListViewModel
    {
        public List<ArtistModel> ArtistList { get; set; }
        public Dictionary<string, int> AlbumsToArtist { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int MaxPages { get; set; }
        public string Sentiment { get; set; }
    }
}