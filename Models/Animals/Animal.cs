using Animaland.Models.Users;
using Newtonsoft.Json;
using System;

namespace Animaland.Models.Animals
{
    public abstract class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }

        public decimal Happiness { get; set; }
        public decimal Hunger { get; set; }

        private const decimal Neutral = 0.0m;

        private const decimal MaxHappiness = 10.0m;
        private const decimal MaxHunger = 10.0m;

        private const decimal MinHappiness = -10.0m;
        private const decimal MinHunger = -10.0m;

        public DateTime LastPetted { get; set; }
        public DateTime LastFed { get; set; }

        [JsonIgnore]
        public abstract decimal HappinessLossRate { get; }
        [JsonIgnore]
        public abstract decimal HungerIncreaseRate { get; }

        protected Animal(string name, int userId)
        {
            Name = name;
            UserId = userId;

            Happiness = Neutral;
            LastPetted = DateTime.Now;

            Hunger = Neutral;
            LastFed = DateTime.Now;
        }

        protected Animal()
        {
            
        }

        public decimal GetHappiness()
        {
            var now = DateTime.Now;
            var interval = now.Subtract(LastPetted);
            var minutesElapsed = (decimal)interval.TotalMinutes;

            var decreasedHappiness = minutesElapsed * HappinessLossRate;

            var updatedHappiness = Happiness - decreasedHappiness;

            return Math.Max(MinHappiness, updatedHappiness);
        }

        public void Pet()
        {
            IncreaseHappiness(1.0m);
            LastPetted = DateTime.Now;
        }

        private void IncreaseHappiness(decimal increment)
        {
            var happiness = GetHappiness();
            happiness += increment;
            Happiness = Math.Min(MaxHappiness, happiness);
        }

        public decimal GetHunger()
        {
            var now = DateTime.Now;
            var interval = now.Subtract(LastFed);
            var minutesElapsed = (decimal)interval.TotalMinutes;

            var increasedHunger = minutesElapsed * HungerIncreaseRate;

            var updatedHunger = Hunger + increasedHunger;

            return Math.Min(MaxHunger, updatedHunger);
        }

        public void Feed()
        {
            DecreaseHunger(1.0m);
            LastFed = DateTime.Now;
        }

        private void DecreaseHunger(decimal decrement)
        {
            var hunger = GetHunger();
            hunger -= decrement;
            Hunger = Math.Max(MinHunger, hunger);
        }
    }
}
