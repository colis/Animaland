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
using System.Text;

namespace Animaland.Test
{
    public class AdoptionTest
    {
        private readonly DataContext _context;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly UserController _userController;
        private readonly AnimalController _animalController;

        public AdoptionTest(ITestOutputHelper output)
        {
            _output = output;

            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            _context = server.Host.Services.GetService(typeof(DataContext)) as DataContext;
            _client = server.CreateClient();

            _userController = new UserController(_context);
            _animalController = new AnimalController(_context);
        }

        [Fact]
        public void AUserCanAdoptAnAnimal()
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == 1);
            var animal = _context.Animals.FirstOrDefault(t => t.Id == 1);

            // Act
            user.Animals.Add(animal);
            animal.UserId = 1;

            // Assert
            Assert.NotEmpty(user.Animals);
        }

        [Fact]
        public void AUserCanAdoptAninalsOfDifferentTypes()
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == 1);
            var dog = _context.Animals.FirstOrDefault(t => t.Id == 1);
            var cat = _context.Animals.FirstOrDefault(t => t.Id == 3);

            // Act
            user.Animals.Add(dog);
            user.Animals.Add(cat);

            // Assert
            Assert.NotEmpty(user.Animals);
            Assert.Equal("Animaland.Models.Animals.Dog", user.Animals.First().ToString());
            Assert.Equal("Animaland.Models.Animals.Cat", user.Animals.Last().ToString());
        }

        [Fact]
        public async Task AUserCannotAdoptTheSameAnimalTwice()
        {
            var jsonString = "{}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/user/1/adopt/1", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Act
            response = await _client.PutAsync($"/api/user/1/adopt/1", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AUserCannotAdoptAnAlreadyAdoptedAnimal()
        {
            var jsonString = "{}";
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/user/1/adopt/1", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

            // Act
            response = await _client.PutAsync($"/api/user/2/adopt/1", httpContent);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
