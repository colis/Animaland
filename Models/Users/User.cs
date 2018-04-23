using System.Collections.Generic;
using Animaland.Models.Animals;

namespace Animaland.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Animal> Animals { get; set; } = new List<Animal>();
    }
}
