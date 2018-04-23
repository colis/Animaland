using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc;
using Animaland.Models.Animals;
using Animaland.Storage;

namespace Animaland.Controllers
{
    [Route("api/[controller]")]
    public class AnimalController : Controller
    {
        private readonly DataContext _context;

        public AnimalController(DataContext context)
        {
            _context = context;

            // Initialise a list of animals.
            if (_context.Animals.Count() == 0)
            {
                _context.Animals.Add(new Dog("Charlie", 0));
                _context.Animals.Add(new Dog("Chuck", 0));
                _context.Animals.Add(new Cat("Terry", 0));
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Animal> GetAll()
        {
            // Get the updated values of Hunger and Happines on every API call.
            foreach (var animal in _context.Animals)
            {
                animal.Hunger = animal.GetHunger();
                animal.Happiness = animal.GetHappiness();
            }
            return _context.Animals.ToList();
        }

        [HttpGet("{id}", Name = "GetAnimal")]
        public IActionResult GetById(long id)
        {
            var item = _context.Animals.FirstOrDefault(t => t.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            // Get the updated values of Hunger and Happines on every API call.
            item.Hunger = item.GetHunger();
            item.Happiness = item.GetHappiness();

            return new ObjectResult(item);
        }
    }
}
