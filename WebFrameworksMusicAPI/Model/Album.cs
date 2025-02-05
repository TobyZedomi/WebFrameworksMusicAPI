using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace WebFrameworksMusicAPI.Model
{
    public class Album
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string AlbumName { get; set; }

        public int NumberOfSongs { get; set; }

        public int NumberOfFeatures { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int ArtistId { get; set; }
        public Artist? Artist { get; set; }

        public ICollection<Song> Songs { get; set; }

    }
}
