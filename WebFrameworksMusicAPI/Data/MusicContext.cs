using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebFrameworksMusicAPI.Model;

namespace WebFrameworksMusicAPI.Data
{
    public class MusicContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Album> Album { get; set; }

        public DbSet<Artist> Artist { get; set; }

        public DbSet<Song> Song { get; set; }


        public MusicContext(DbContextOptions options) : base(options)
        {

            Database.EnsureCreated();

        }


    }
}
