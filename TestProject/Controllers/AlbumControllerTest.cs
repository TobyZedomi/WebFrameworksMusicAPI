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

            // making sure to return an BadRequest as artistid doesnt exist

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);


        }

        // test put/update where its a success

        [Fact]
        public async Task UpdateAlbum_WhereUpdateIsASuccess()
        {
            // creating the updated album

            var album = new Album { Id = 3, AlbumName = "testData", NumberOfSongs = 12, ReleaseDate = new DateTime(1975 - 09 - 18), ArtistId = 3 };
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling the put request in the album api 

            var response = await _client.PutAsync($"/api/Albums/{album.Id}", content);


            // making sure the response was a success

            response.EnsureSuccessStatusCode();

            // making sure the no content response is equal tot he reponse from the put request

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);


            // get album by id to see if they match

            var response2 = await _client.GetAsync($"/api/Albums/{album.Id}");
            var responseString = await response2.Content.ReadAsStringAsync();
            var albumConfirm = JsonConvert.DeserializeObject<Album>(responseString);


            // checking if updated album and hard coded album match each other

            Assert.Equal(album.Id, albumConfirm.Id);
            Assert.Equal(album.AlbumName, albumConfirm.AlbumName);
            Assert.Equal(album.NumberOfSongs, albumConfirm.NumberOfSongs);
            Assert.Equal(album.ReleaseDate, albumConfirm.ReleaseDate);
            Assert.Equal(album.ArtistId, albumConfirm.ArtistId);

        }


        // test put.update if albumId doesnt exist 


        [Fact]
        public async Task UpdateAlbum_WhereAlbumIdDoesntExist()
        {
            // creating the updated album

            var album = new Album { Id = 32343, AlbumName = "testData123", NumberOfSongs = 12, ReleaseDate = new DateTime(1975 - 09 - 18), ArtistId = 3 };
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling the put request in the album api 

            var response = await _client.PutAsync($"/api/Albums/{album.Id}", content);

            // making sure the not found response is equal to the reponse from the put request

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

        }


        [Fact]
        public async Task UpdateAlbum_WhereArtistIdDoesntExist()
        {
            // creating the updated album

            var album = new Album { Id = 3, AlbumName = "testData123", NumberOfSongs = 12, ReleaseDate = new DateTime(1975 - 09 - 18), ArtistId = 34254 };
            var content = new StringContent(JsonConvert.SerializeObject(album), Encoding.UTF8, "application/json");

            // calling the put request in the album api 

            var response = await _client.PutAsync($"/api/Albums/{album.Id}", content);

            // making sure to return an InternalServerError as artistid doesnt exist

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }


        // test for delete by id 

        [Fact]
        public async Task DeleteAlbum_WhereAlbumIsDeleted()
        {
            var albumId = 5;

            // calling the delete request

            var response = await _client.DeleteAsync($"/api/Albums/{albumId}");

            // using get request to check if album id still exist 

            var response2 = await _client.GetAsync($"/api/Albums/{albumId}");

            //making sure it returns not found along with the get request

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response2.StatusCode);

        }


        // test for delete request but id doesnt exist 


        [Fact]
        public async Task DeleteAlbum_WhereAlbumIdDoesntExist()
        {
            var albumId = 52332;

            // calling the delete request

            var response = await _client.DeleteAsync($"/api/Albums/{albumId}");

            // making sure the no content response is equal to the reponse from the delete request

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);


        }

    }
}
