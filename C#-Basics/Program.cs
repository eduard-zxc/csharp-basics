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

        public void ShowAnimalInfo(bool showAge = true, bool showDiet = false)
        {
            foreach (var animal in animals)
            {
                string output = $"{animal.Name}";

                if (showAge)
                    output += $" ({animal.Age} years old)";

                if (showDiet)
                {
                    if (animal is Lion)
                        output += " - eats meat";
                    else if (animal is Parrot)
                        output += " - eats seeds";
                }

                Console.WriteLine(output);

                Parrot parrot = animal as Parrot;
                if (parrot != null)
                {
                    Console.WriteLine($"{parrot.Name} says: Hello!\n");
                }
                else Console.WriteLine("Not a parrot - can't talk\n");
            }
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

            myZoo.ShowAnimalInfo(showDiet: true);

            var Lion = new Lion("Mufasa", 2, true);
            Console.WriteLine("Zoo animals:\n");

            foreach (Animal animal in myZoo)
            {
                Console.WriteLine($"Name: {animal.Name}, Age: {animal.Age}");
                animal.MakeSound();
                animal.Feed();
                animal.Feed("fruits");

                if (animal is Lion lion)
                {
                    Console.WriteLine($"Is Alpha: {lion.IsAlpha}");
                }
                else if (animal is Parrot parrot)
                {
                    Console.WriteLine($"Color: {parrot.Color}");
                }

                Animal clone = (Animal)animal.Clone();
                Console.WriteLine($"Cloned: {clone.Name} (Age: {clone.Age})\n");
            }
        }
    }
}