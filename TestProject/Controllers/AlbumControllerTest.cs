using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFrameworksMusicAPI;
using WebFrameworksMusicAPI.Model;

namespace TestProject.Controllers
{
    public class AlbumControllerTest : IClassFixture<DataWebApplicationFactory<Program>>
    {

        private readonly HttpClient _client;

        public AlbumControllerTest(DataWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

        }


        // test for get album by id 


        [Fact]
        public async Task GetAlbumById_WhereAlbumExist()
        {

            var albumId = 1;

            // getting album by id with using get request 

            var response = await _client.GetAsync($"/api/Albums/{albumId}");

            // making sure the request was a success and deserializing the json object that returns 

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var album = JsonConvert.DeserializeObject<Album>(responseString);

            // making sure the Ids are equal
            Assert.Equal(albumId, album.Id);

        }


        // test for get album where id is not found

        [Fact]
        public async Task GetAlbumById_WhereAlbumIdDoesntExist()
        {

            var albumId = 1432;

            // getting album by id with using get request 

            var response = await _client.GetAsync($"/api/Albums/{albumId}");

            // making sure the response from the get uis equal to not found

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }


        // test for post album where added a success

        [Fact]
        public async Task PostAlbum_WhereAlbumIsMade()
        {

            /// creating a new album

            var album = new Album { AlbumName = "Fullingness", NumberOfSongs = 12, ReleaseDate = new DateTime(1975 - 09 - 18), ArtistId = 2 };
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling post request to add album

            var response = await _client.PostAsync("/api/Albums", content);

            // making sure the post request was a success

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var albumConfirm = JsonConvert.DeserializeObject<Album>(responseString);

            // making sure the post request returns a created 

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);


            // checking both artist expected result and artist from post is the same 


            Assert.Equal(album.AlbumName, albumConfirm.AlbumName);
            Assert.Equal(album.NumberOfSongs, albumConfirm.NumberOfSongs);
            Assert.Equal(album.ReleaseDate, albumConfirm.ReleaseDate);
            Assert.Equal(album.ArtistId, albumConfirm.ArtistId);

        }


        // test post where album id already exist

        [Fact]

        public async Task PostAlbum_WhereAlbumIdAlreadyExist()
        {
            /// creating a new album

            var album = new Album {Id = 1, AlbumName = "Fullingness", NumberOfSongs = 12, ReleaseDate = new DateTime(1975 - 09 - 18), ArtistId = 2 };
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling post request to add album

            var response = await _client.PostAsync("/api/Albums", content);

            // making sure to return an InternalServerError as albumid already exist

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


        }

        // test post where artist id doesnt exist

        [Fact]  
        public async Task PostAlbum_WhereArtistIdDoesntExist()
        {
            /// creating a new album

            var album = new Album { AlbumName = "Fullingness", NumberOfSongs = 12, ReleaseDate = new DateTime(1975 - 09 - 18), ArtistId = 2445 };
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling post request to add album

            var response = await _client.PostAsync("/api/Albums", content);

            // making sure to return an InternalServerError as artistid doesnt exist

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


        }


        // test post where its null


        [Fact]
        public async Task PostAlbum_WhereAlbumIsNull()
        {
            /// creating a new album

            var album = new Album ();
            album = null;
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling post request to add album

            var response = await _client.PostAsync("/api/Albums", content);

            // making sure to return an InternalServerError as artistid doesnt exist

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);


        }


        

    }
}
