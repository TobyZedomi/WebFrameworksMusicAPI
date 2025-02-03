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

        [Range(0,50)]
        public int NumberOfSongs { get; set; }

        [Range(0,50)]
        public int NumberOfFeatures { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Artist? Artist { get; set; }
    }
}
