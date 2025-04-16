using System;
using Script.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Main
{
    public class GameManager : MonoBehaviour
    {
        public Character currentCharacter;

        void Start()
        {
            print(GameData.GameName);
        }
    }
}