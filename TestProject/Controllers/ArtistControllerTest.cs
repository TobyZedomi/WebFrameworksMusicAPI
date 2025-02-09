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
    public class ArtistControllerTest : IClassFixture<DataWebApplicationFactory<Program>>
    {


        private readonly HttpClient _client;

        public ArtistControllerTest(DataWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

        }


        // test for artist by Id where Id exist

        [Fact]
        public async Task GetArtistById_WhereArtistExist()
        {

            var artistId = 1;

            // getting artist by id with using get request 

            var response = await _client.GetAsync($"/api/Artists/{artistId}");

            // making sure the request was a success and deserializing the json object that returns 

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var artist = JsonConvert.DeserializeObject<Artist>(responseString);

            // making sure the Ids are equal
            Assert.Equal(artistId, artist.Id);

        }


        // test for artist id that doesnt exist

        [Fact]  
        public async Task GetArtistById_WhereIdDoesntExist()
        {

            var artistId = 5465;

            // using the get request to get artist by id

            var response = await _client.GetAsync($"/api/Artists/{artistId}");

            // making sure the response returned was not found 

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
        }


        // test for post artist where added a success

        [Fact]
        public async Task PostArtist_WhereArtistIsMade()
        {

            /// creating a new artist

            var artist = new Artist { ArtistName = "Janet Jackson", Genre = "RnB/Soul", DateOfBirth = new DateTime(1963 - 09 - 23) };
            var content = new StringContent(JsonConvert.SerializeObject(artist), Encoding.UTF8, "application/json");

            // calling post request to add artist

            var response = await _client.PostAsync("/api/Artists", content);

            // making sure the post request was a success

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var artistConfirm = JsonConvert.DeserializeObject<Artist>(responseString);

            // checking both artist expected result and artist from post is the same 
            
            Assert.Equal(artist.ArtistName, artistConfirm.ArtistName);
            Assert.Equal(artist.Genre, artistConfirm.Genre);
            Assert.Equal(artist.DateOfBirth, artistConfirm.DateOfBirth);    

        }


        // test for post artist where artist id already exist

        [Fact]
        public async Task PostArtist_WhereArtistIdAlreadyExist()
        {

            /// creating a new artist

            var artist = new Artist { Id = 1, ArtistName = "Janet Jackson", Genre = "RnB/Soul", DateOfBirth = new DateTime(1963 - 09 - 23) };
            var content = new StringContent(JsonConvert.SerializeObject(artist), Encoding.UTF8, "application/json");

            // calling post request to add artist

            var response = await _client.PostAsync("/api/Artists", content);

            // making sure the post request was a not a success by making sure the internal server error is equal to the response

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);


        }


        // test for post when artist added is null

        [Fact]
        public async Task PostArtist_WhereArtistIsNull()
        {

            /// creating a new artist taht is null

            var artist = new Artist();
            artist = null;

            var content = new StringContent(JsonConvert.SerializeObject(artist), Encoding.UTF8, "application/json");

            // calling post request to add artist

            var response = await _client.PostAsync("/api/Artists", content);

            // making sure the post request was a not a success by making sure the bad request error is equal to the response

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);


        }


        // test for put/update 


        [Fact]

        public async Task UpdateArtist_WhereUpdateIsASuccess()
        {
            // creating the updated artist 

            var artist = new Artist { Id = 3, ArtistName = "Miles Davis", Genre = "Jazz", DateOfBirth = new DateTime(1963 - 09 - 23) };
            var content = new StringContent(JsonConvert.SerializeObject(artist), Encoding.UTF8, "application/json");

            // calling the put request in the artist api 

            var response = await _client.PutAsync($"/api/Artists/{artist.Id}", content);


            // making sure the response was a success

            response.EnsureSuccessStatusCode();

            // making sure the no content response is equal tot he reponse from the put request

            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);


            // get artist by id to see if they match

            var response2 = await _client.GetAsync($"/api/Artists/{artist.Id}");
            var responseString = await response2.Content.ReadAsStringAsync();
            var artistConfirm = JsonConvert.DeserializeObject<Artist>(responseString);


            // checking if updated artist and hard coded artist match each other

            Assert.Equal(artist.Id, artistConfirm.Id);
            Assert.Equal(artist.ArtistName, artistConfirm.ArtistName);
            Assert.Equal(artist.Genre, artistConfirm.Genre);
            Assert.Equal(artist.DateOfBirth, artistConfirm.DateOfBirth);

        }


        // test for put/update where artistId doesnt exist 


        [Fact]

        public async Task UpdateArtist_WhereArtistIdDoesntExist()
        {
            // creating the updated artist with id that doesnt exist

            var artist = new Artist { Id = 3433, ArtistName = "Miles Davis", Genre = "Jazz", DateOfBirth = new DateTime(1963 - 09 - 23) };
            var content = new StringContent(JsonConvert.SerializeObject(artist), Encoding.UTF8, "application/json");

            // calling the put request in the artist api 

            var response = await _client.PutAsync($"/api/Artists/{artist.Id}", content);

            // making sure the not found response is equal to the reponse from the put request

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
           
        }

        // test for delete by id 

        [Fact]
        public async Task DeleteArtist_WhereArtistIsDeleted()
        {
            var artistId = 5;

            // calling the delete request

            var response = await _client.DeleteAsync($"/api/Artists/{artistId}");

            // using get request to hceck if artist id still exist 

            var response2 = await _client.GetAsync($"/api/Artists/{artistId}");

            //making sure it returns not found along with the get request

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response2.StatusCode);

        }


        // test delete where artist id doesnt exist 

        [Fact]

        public async Task DeleteArtist_WhereArtistIdDoesntExist()
        {
            var artistId = 325;

            // calling the delete request

            var response = await _client.DeleteAsync($"/api/Artists/{artistId}");

            // making sur ethe no content response is equal to the reponse from the delete request

            Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);

        }


    }
}
