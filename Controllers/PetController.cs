using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Animaland.Storage;

namespace Animaland.Controllers
{
    public class PetController : Controller
    {
        private readonly DataContext _context;

        public PetController(DataContext context)
        {
            _context = context;
        }

        [HttpPut("api/animal/{animalId}/pet", Name = "PetAnimal")]
        public IActionResult Update([FromRoute] int animalId)
        {
            var animal = _context.Animals.FirstOrDefault(t => t.Id == animalId);

            if (animal == null)
            {
                return NotFound();
            }

            animal.Pet();

            _context.SaveChanges();

            return new NoContentResult();
        }
    }
}
