using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Xunit;
using Microsoft.AspNetCore.TestHost;
using Animaland.Storage;
using System.Linq;
using Xunit.Abstractions;
using Animaland.Controllers;

namespace Animaland.Test
{
    public class PetTest
    {
        private readonly DataContext _context;
        private readonly HttpClient _client;
        private readonly ITestOutputHelper _output;
        private readonly AnimalController _animalController;

        public PetTest(ITestOutputHelper output)
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
        public void AnAnimalStartsWithANeutralHappinessLevel()
        {
            var animal = _context.Animals.FirstOrDefault(t => t.Id == 1);

            // Assert
            Assert.Equal(0.0m, animal.Happiness);
        }

        [Fact]
        public void PettingAnAnimalWillIncreaseTheHappinessLevel()
        {
            var animal = _context.Animals.FirstOrDefault(t => t.Id == 1);

            // Act
            animal.Pet();

            // Assert
            Assert.NotEqual(0.0m, animal.GetHappiness());
        }

        [Fact]
        public void TheHappinessLevelWillDecreaseOverTime()
        {
            var animal = _context.Animals.FirstOrDefault(t => t.Id == 1);

            // Assert
            Assert.NotEqual(0.0m, animal.GetHappiness());
        }
    }
}
