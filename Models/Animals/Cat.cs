using System;

namespace Animaland.Models.Animals
{
    public class Cat : Animal
    {
        public Cat(string name, int userId)
            : base(name, userId)
        {
        }

        public Cat()
        {
        }

        public override decimal HappinessLossRate { get { return 0.1m; } }
        public override decimal HungerIncreaseRate { get { return 0.2m; } }
    }
}
