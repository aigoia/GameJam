using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Main
{
    public class MainData : MonoBehaviour
    {
        public List<TileNode> currentTileList = new List<TileNode>();

        TileManager _tileManager;
        
        void Awake()
        {
            currentTileList = FindObjectsByType<TileNode>(FindObjectsSortMode.None).ToList();
        }
    }
}
