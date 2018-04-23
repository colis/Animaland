using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Animaland.Storage;
using Animaland.Models.Animals;

namespace Animaland.Controllers
{
    public class AdoptionController : Controller
    {
        private readonly DataContext _context;

        public AdoptionController(DataContext context)
        {
            _context = context;
        }

        [HttpPut("api/user/{userId}/adopt/{animalId}", Name = "AdoptAnimal")]
        public IActionResult Update([FromRoute] int userId, [FromRoute] int animalId)
        {
            var user = _context.Users.FirstOrDefault(t => t.Id == userId);
            var animal = _context.Animals.FirstOrDefault(t => t.Id == animalId);

            if (user == null || animal == null)
            {
                return NotFound();
            }

            // An animal can't be owned by multiple users or twice by the same user.
            if (animal.UserId != 0)
            {
                return BadRequest();
            }

            // Add the adopted animal to the Animals list of the new owner.
            user.Animals.Add(animal);
            // Update the animal's user id foreign key.
            animal.UserId = userId;

            _context.SaveChanges();
            
            return new NoContentResult();
        }
    }
}
