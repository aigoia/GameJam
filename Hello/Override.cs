using System;
using Script.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Hello
{
    public class Override : MonoBehaviour
    {
        void Start()
        {
            Dog redDog = new Dog(Color.red, 2);
            Dog blueDog = new Dog(Color.blue, 3);
            
            // redDog.Speak();
            // blueDog.Speak();

            Cat redCat = new Cat(Color.red);
            Cat blueCat = new Cat(Color.blue);
            
            // redCat.Speak();
            // blueCat.Speak();

            print($"Age: {redDog.Age}");
            print($"Age: {blueDog.Age}");

            redDog.Royalty = 2;
            print($"Royalty: {redDog.Royalty}");
            blueDog.Royalty = 0;
            print($"Royalty: {blueDog.Royalty}");
        }
    }
    
    public class Animal
    {
        protected Color Color;
        protected int Age;
        
        public virtual void Speak() {
            Show.Print("Some sound");
        }

        protected Animal(Color color, int age)
        {
            Color = color;
            Age = age;
        }
    }

    public class Dog : Animal
    {
        public new int Age { get;}
        public int Royalty { get; set; }

        public Dog(Color color, int age) : base(color, age)
        {
            Age = age;
        }

        public override void Speak()
        {
            Show.Print($"Bark! My color is {Color}");
        }
    }

    public class Cat : Animal
    {
        public Cat(Color color) : base(color, age: 0)
        {
            // base initialize
        }

        public override void Speak()
        {
            Show.Print($"Meow! My color is {Color}");
        }
    }
}