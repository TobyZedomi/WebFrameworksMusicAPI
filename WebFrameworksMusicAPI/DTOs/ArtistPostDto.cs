using System.ComponentModel.DataAnnotations;

namespace WebFrameworksMusicAPI.DTOs
{
    public class ArtistPostDto
    {

        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string Genre { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
