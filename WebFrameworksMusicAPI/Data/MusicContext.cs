using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebFrameworksMusicAPI.Model;

namespace WebFrameworksMusicAPI.Data
{
    public class MusicContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Album> Albums { get; set; }

        public DbSet<Artist> Artists { get; set; }

        public DbSet<Song> Songs { get; set; }


        public MusicContext(DbContextOptions options) : base(options)
        {

            Database.EnsureCreated();

        }


    }
}
