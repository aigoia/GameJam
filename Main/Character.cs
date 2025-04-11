using System;
using Script.Common;
using UnityEngine;

namespace Script.Main
{
    public class Character : MonoBehaviour
    {
        MainData _mainData;
        PathFinding _pathFinding;

        void Awake()
        {
            _mainData = FindObjectOfType<MainData>();
            _pathFinding = GetComponent<PathFinding>();
        }

        void Start()
        {
            var map = _mainData.currentTileList;

            var startNode = map.Find(i => i.tileCoordinate == Vector2.zero);
            var endNode = map.Find(i => i.tileCoordinate == new Vector2(5, 5));
            
            
            // var result = _pathFinding.GreedPathFinding(startNode, endNode, map);
            var result = _pathFinding.AStarPathFinding(startNode, endNode, map);
            Show.PrintList(result);
            // result.Add(startNode);
            _pathFinding.PrintWay(result);
        }
    }
}
