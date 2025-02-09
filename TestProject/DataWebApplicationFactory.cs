using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFrameworksMusicAPI.Data;
using WebFrameworksMusicAPI.Model;

namespace TestProject
{
    public class DataWebApplicationFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MusicContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<MusicContext>(options =>
                {
                    options.UseSqlServer(GetConnectionString());
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<MusicContext>();

                try
                {

                    db.Database.Migrate();

                    SeedData(db);


                }
                catch (Exception ex)
                {

                    var logger = scopedServices.GetRequiredService<ILogger<DataWebApplicationFactory<TStartUp>>>();
                    logger.LogError(ex, "An error happened in the database");
                }


            });
        }

        private string GetConnectionString()
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration.GetConnectionString("DefaultConnection1");
        }

        private void SeedData(MusicContext db)
        {
            if (!db.Artist.Any())
            {
                db.Artist.AddRange(

                new Artist { Id = 1, ArtistName = "Ken Carson", Genre = "HipHop" },
                new Artist { Id = 2, ArtistName = "Stevie Wonder", Genre = "RnB/Soul" },
                new Artist { Id = 3, ArtistName = "John Coltrane", Genre = "Jazz" }

                    );

                db.SaveChanges();
            }
        }


    }
}
