using System;
using System.Collections.Generic;
using Script.Common;
using UnityEngine;

namespace Script.Main
{
    public class Character : MonoBehaviour
    {
        public ActiveState activeState;
        
        MainData _mainData;
        PathFinding _pathFinding;
        CharacterMove _characterMove;
        
        void Awake()
        {
            _mainData = FindObjectOfType<MainData>();
            _pathFinding = FindObjectOfType<PathFinding>();
            _characterMove = GetComponent<CharacterMove>();
        }

        void Start()
        {
            var map = _mainData.currentTileList;

            var startNode = map.Find(i => i.tileCoordinate == Vector2.zero);
            var endNode = map.Find(i => i.tileCoordinate == new Vector2(8, 2));
            
            
            var result = _pathFinding.AStarPathFinding(startNode, endNode, map);
            // var result = _pathFinding.AStarPathFinding(startNode, endNode, map);
            // Show.PrintList(result);
            // result.Add(startNode);
            _pathFinding.PrintWay(result);

            _ = _characterMove.IndicateUnit(result);

        }
    }

    public enum CharacterType
    {
        None, Closed, Ranged,
    }
    
    public enum ActiveState
    {
        Moving,
        NotAnything
    }
}
