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
    public class UserTest
    {
        private readonly DataContext _context;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly UserController _userController;

        public UserTest(ITestOutputHelper output)
        {
            _output = output;

            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            _context = server.Host.Services.GetService(typeof(DataContext)) as DataContext;
            _client = server.CreateClient();

            _userController = new UserController(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            var users = _context.Users;

            // Act
            var response = await _client.GetAsync($"/api/user");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(3, users.Count());
        }

        [Fact]
        public async Task GetById_ShouldReturnTheSelectedUser()
        {
            var user_one = _context.Users.FirstOrDefault(t => t.Id == 1);

            // Act
            var response = await _client.GetAsync($"/api/user/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, user_one.Id);
            Assert.Equal("Carmine", user_one.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound()
        {
            // Act
            var response = await _client.GetAsync($"/api/user/4");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
