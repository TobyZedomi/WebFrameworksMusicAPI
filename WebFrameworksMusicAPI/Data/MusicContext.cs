using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Artist>()
               .HasData(
               new Artist
               {
                   Id = 1,
                   ArtistName = "Ken Carson",
                   Genre = "HipHop",
                   DateOfBirth = new DateTime(2000-04-17),
               }
               );

            modelBuilder.Entity<Artist>()
               .HasData(
               new Artist
               {
                   Id = 2,
                   ArtistName = "Stevie Wonder",
                   Genre = "RnB/Soul",
                   DateOfBirth = new DateTime(1950 - 05 - 17),
               }
               );

            modelBuilder.Entity<Artist>()
               .HasData(
               new Artist
               {
                   Id = 3,
                   ArtistName = "John Coltrane",
                   Genre = "Jazz",
                   DateOfBirth = new DateTime(1926 - 09 - 23),
               }
               );

            modelBuilder.Entity<Artist>()
               .HasData(
               new Artist
               {
                   Id = 4,
                   ArtistName = "Thom Yorke",
                   Genre = "Rock",
                   DateOfBirth = new DateTime(1968 - 09 - 14),
               }
               );

            modelBuilder.Entity<Artist>()
               .HasData(
               new Artist
               {
                   Id = 5,
                   ArtistName = "Nas",
                   Genre = "HipHop",
                   DateOfBirth = new DateTime(1973 - 09 - 14),
               }
               );


            // album data

            modelBuilder.Entity<Album>()
               .HasData(
               new Album
               {
                   Id = 1,
                   AlbumName = "A Great Chaos",
                   NumberOfSongs = 18,
                   NumberOfFeatures = 4,
                   ReleaseDate = new DateTime(2023 - 10 - 23),
                   ArtistId = 1,
               }
               );

            modelBuilder.Entity<Album>()
               .HasData(
               new Album
               {
                   Id = 2,
                   AlbumName = "Songs In The Key Of Life",
                   NumberOfSongs = 21,
                   NumberOfFeatures = 7,
                   ReleaseDate = new DateTime(1976 - 09 - 28),
                   ArtistId = 2,
               }
               );

            modelBuilder.Entity<Album>()
             .HasData(
             new Album
             {
                 Id = 3,
                 AlbumName = "Blue Train",
                 NumberOfSongs = 5,
                 NumberOfFeatures = 0,
                 ReleaseDate = new DateTime(1958 - 01 - 23),
                 ArtistId = 3,
             }
             );

            modelBuilder.Entity<Album>()
             .HasData(
             new Album
             {
                 Id = 4,
                 AlbumName = "OK Computer",
                 NumberOfSongs = 12,
                 NumberOfFeatures = 0,
                 ReleaseDate = new DateTime(1997 - 05 - 21),
                 ArtistId = 4,
             }
             );

            modelBuilder.Entity<Album>()
            .HasData(
            new Album
            {
                Id = 5,
                AlbumName = "In Rainbows",
                NumberOfSongs = 10,
                NumberOfFeatures = 0,
                ReleaseDate = new DateTime(2007 - 10 - 10),
                ArtistId = 4,
            }
            );

            modelBuilder.Entity<Album>()
            .HasData(
            new Album
            {
                Id = 6,
                AlbumName = "illmatic",
                NumberOfSongs = 9,
                NumberOfFeatures = 1,
                ReleaseDate = new DateTime(1994 - 04 - 19),
                ArtistId = 5,
            }
            );

            // Song Data

            modelBuilder.Entity<Song>()
            .HasData(
            new Song
            {
                Id = 1,
                SongName = "Nightcore",
                ArtistId = 1,
                AlbumId = 1,
                
            }
            );

            modelBuilder.Entity<Song>()
           .HasData(
           new Song
           {
               Id = 2,
               SongName = "As",
               ArtistId = 2,
               AlbumId = 2,

           }
           );

            modelBuilder.Entity<Song>()
           .HasData(
           new Song
           {
               Id = 3,
               SongName = "Moments Notice",
               ArtistId = 3,
               AlbumId = 3,

           }
           );


            modelBuilder.Entity<Song>()
           .HasData(
           new Song
           {
               Id = 4,
               SongName = "Let Down",
               ArtistId = 4,
               AlbumId = 4,

           }
           );


            modelBuilder.Entity<Song>()
           .HasData(
           new Song
           {
               Id = 5,
               SongName = "Airbag",
               ArtistId = 4,
               AlbumId = 4,

           }
           );


            modelBuilder.Entity<Song>()
           .HasData(
           new Song
           {
               Id = 6,
               SongName = "Jigsaws",
               ArtistId = 4,
               AlbumId = 5,

           }
           );



            modelBuilder.Entity<Song>()
           .HasData(
           new Song
           {
               Id = 7,
               SongName = "Represent",
               ArtistId = 5,
               AlbumId = 6,

           }
           );






            // User data


            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserRole<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityUser<string>>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
