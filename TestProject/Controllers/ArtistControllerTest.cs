using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFrameworksMusicAPI;

namespace TestProject.Controllers
{
    public class ArtistControllerTest : IClassFixture<DataWebApplicationFactory<Program>>
    {


        private readonly HttpClient _client;

        public ArtistControllerTest(DataWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

        }



    }
}
