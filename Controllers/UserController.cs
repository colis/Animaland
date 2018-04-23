using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Animaland.Models.Users;
using Animaland.Storage;
using Microsoft.EntityFrameworkCore;

namespace Animaland.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;

            // Initialise a list of users.
            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { Name = "Carmine" });
                _context.Users.Add(new User { Name = "Bob" });
                _context.Users.Add(new User { Name = "Alice" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            // Get the updated values of Hunger and Happines on every API call.
            foreach (var animal in _context.Animals)
            {
                animal.Hunger = animal.GetHunger();
                animal.Happiness = animal.GetHappiness();
            }
            return _context.Users.Include("Animals").ToList();
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var user = _context.Users.Include("Animals").FirstOrDefault(t => t.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            // Get the updated values of Hunger and Happines for the owned animals on every API call.
            var ownedAnimals = _context.Animals.Where(t => t.UserId == user.Id);
            foreach (var animal in ownedAnimals)
            {
                animal.Hunger = animal.GetHunger();
                animal.Happiness = animal.GetHappiness();
            }
            return new ObjectResult(user);
        }
    }
}
