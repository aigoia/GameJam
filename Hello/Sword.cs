using UnityEngine;

namespace Script.Hello
{
    public class Sword
    {
        public string Name { get; }
        public int Power { get; }

        public Sword(string name, int power)
        {
            Name = name;
            Power = power;
        }
    }
}
