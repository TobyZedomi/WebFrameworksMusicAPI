﻿using Newtonsoft.Json;
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






    }
}
