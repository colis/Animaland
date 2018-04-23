using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Animaland.Storage;

namespace Animaland.Controllers
{
    public class FeedController : Controller
    {
        private readonly DataContext _context;

        public FeedController(DataContext context)
        {
            _context = context;
        }

        [HttpPut("api/animal/{animalId}/feed", Name = "FeedAnimal")]
        public IActionResult Update([FromRoute] int animalId)
        {
            var animal = _context.Animals.FirstOrDefault(t => t.Id == animalId);

            if (animal == null)
            {
                return NotFound();
            }

            animal.Feed();

            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
