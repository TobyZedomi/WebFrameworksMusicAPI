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


        //


    }
}
