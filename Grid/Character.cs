using System;
using System.Collections.Generic;
using Script.Common;
using UnityEngine;

namespace Script.Grid
{
    public class Character: MonoBehaviour
    { 
        CharacterMove _characterMove;
        PathFinding  _pathFinding;

        void Awake()
        {
            _characterMove = GetComponent<CharacterMove>();
            _pathFinding = GetComponent<PathFinding>();
        }

        void Start()
        {
            TileNode nodeOne = gameObject.AddComponent<TileNode>();
            nodeOne.Init(new Vector2(1, 1));
            TileNode nodeTwo = gameObject.AddComponent<TileNode>();
            nodeTwo.Init(new Vector2(2, 2));
            TileNode nodeThree = gameObject.AddComponent<TileNode>();
            nodeThree.Init(new Vector2(2, 3));
            TileNode nodeFour = gameObject.AddComponent<TileNode>();
            nodeFour.Init(new Vector2(2, 4));
            
            var result = new List<TileNode>()
            {
                nodeOne,
                nodeTwo,
                nodeThree,
                nodeFour,
            };
            
            Show.PrintList(result);
            
            _ = _characterMove.GoUnit(result);
        }
    }
}