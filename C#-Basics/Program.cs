using System;
using System.Collections;
using System.Collections.Generic;

namespace ZooApp
{
    public class Animal : ICloneable
    {
        private string _name;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public int Age { get; set; }

        public Animal(string name, int age)
        {
            _name = name;
            Age = age;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine($"{Name} makes a sound.");
        }

        public void Feed()
        {
            Console.WriteLine($"{Name} is being fed.");
        }

        public void Feed(string food)
        {
            Console.WriteLine($"{Name} is eating {food}.");
        }

        public virtual object Clone()
        {
            return new Animal(Name, Age);
        }
    }

    public class Lion : Animal
    {
        public bool IsAlpha { get; set; }

        public Lion(string name, int age, bool isAlpha)
            : base(name, age) => IsAlpha = isAlpha;

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} roars loudly!");
        }

        public override object Clone()
        {
            return new Lion(Name, Age, IsAlpha);
        }
    }

    public class Parrot : Animal
    {
        public string Color { get; set; }

        public Parrot(string name, int age, string color)
            : base(name, age) => Color = color;

        public override void MakeSound()
        {
            Console.WriteLine($"{Name} says: 'Hello! Hello!'");
        }

        public override object Clone()
        {
            return new Parrot(Name, Age, Color);
        }
    }

    public class Zoo : IEnumerable<Animal>
    {
        private List<Animal> animals = [];

        public void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public IEnumerator<Animal> GetEnumerator()
        {
            return animals.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class Program
    {
        static void Main()
        {
            Zoo myZoo = new Zoo();

            myZoo.AddAnimal(new Lion("Simba", 5, true));
            myZoo.AddAnimal(new Parrot("Polly", 2, "Green"));
            myZoo.AddAnimal(new Animal("Generic Animal", 3));

            Console.WriteLine("Zoo animals:\n");

            foreach (Animal animal in myZoo)
            {
                Console.WriteLine($"Name: {animal.Name}, Age: {animal.Age}");
                animal.MakeSound();
                animal.Feed();
                animal.Feed("fruits");

                Animal clone = (Animal)animal.Clone();
                Console.WriteLine($"Cloned: {clone.Name} (Age: {clone.Age})\n");
            }
        }
    }
}