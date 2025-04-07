using System.Collections.Generic;
using Script.Common;
using UnityEditor.Playables;
using UnityEngine;

namespace Script.Hello
{
    public class HelloWorld : MonoBehaviour
    {
        readonly List<(string Name, int Count)> _fruitList = new(){("Orange", 0), ("Apple", 1), ("Banana", 0), ("Cherry", 1)};
        readonly List<int> _intList = new(){ 1, 2, 3, 4, 5};

        int One => 1;
        int Two => 2;
        float ThreeHalf => 3.5f;
        float FourHalf => 4.5f;
        
        void Start()
        {
            // Show.PrintList(_fruitList);
            Sword orangeSword = new("Orange Sword", One);
            Sword appleSword = new("Apple Sword", Two);
            
            print(orangeSword.Name);
            print(orangeSword.Power);
        }


        int Plus(int a, int b)
        {
            return a + b;
        }

        float PlusFloat(float a, float b)
        {
            return a + b;
        }

        void SayHello()
        {
            Show.Print("Hello");
        }
        
    }
}
