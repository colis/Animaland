using Microsoft.EntityFrameworkCore;
using Animaland.Models.Users;
using Animaland.Models.Animals;
using System.Collections.Generic;

namespace Animaland.Storage
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Cat> Cats { get; set; }
    }
}
