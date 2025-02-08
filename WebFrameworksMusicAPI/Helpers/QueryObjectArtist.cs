namespace WebFrameworksMusicAPI.Helpers
{
    public class QueryObjectArtist
    {

        public string? ArtistName { get; set; } = null;
        public string? Genre { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
    }
}
