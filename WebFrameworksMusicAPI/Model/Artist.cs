using System.ComponentModel.DataAnnotations;

namespace WebFrameworksMusicAPI.Model
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string ArtistName { get; set; }

        [MaxLength(50)]
        public string Genre { get; set; }

        public DateTime DateOfBirth { get; set; }

        public ICollection<Album> albums { get; set; }

    }
}
