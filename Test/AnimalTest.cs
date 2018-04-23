using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Animaland.Storage;
using System.Linq;
using Xunit.Abstractions;
using Animaland.Controllers;

namespace Animaland.Test
{
    public class AnimalTest
    {
        private readonly DataContext _context;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly AnimalController _animalController;

        public AnimalTest(ITestOutputHelper output)
        {
            _output = output;

            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            _context = server.Host.Services.GetService(typeof(DataContext)) as DataContext;
            _client = server.CreateClient();

            _animalController = new AnimalController(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllAnimals()
        {
            var animals = _context.Animals;

            // Act
            var response = await _client.GetAsync($"/api/animal");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, animals.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnTheSelectedAnimal()
        {
            var animal_one = _context.Animals.FirstOrDefault(t => t.Id == 1);

            // Act
            var response = await _client.GetAsync($"/api/animal/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, animal_one.Id);
            Assert.Equal("Charlie", animal_one.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound()
        {
            // Act
            var response = await _client.GetAsync($"/api/animal/4");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
