using System;

namespace Animaland.Models.Animals
{
    public class Dog : Animal
    {
        public Dog(string name, int userId)
            : base(name, userId)
        {
        }

        public Dog()
        {
        }

        public override decimal HappinessLossRate { get { return 0.2m; } }
        public override decimal HungerIncreaseRate { get { return 0.4m; } }
        
    }
}
