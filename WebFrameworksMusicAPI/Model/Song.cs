using System.ComponentModel.DataAnnotations;

namespace WebFrameworksMusicAPI.Model
{
    public class Song
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(200)]
        public string SongName { get; set; }
        public Artist? Artist { get; set; }
        public Album? Album { get; set; }

    }
}
