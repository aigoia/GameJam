using System;
using System.Collections.Generic;
using System.Linq;
using Script.Common;
using UnityEngine;

namespace Script.Grid
{
    public class PathFinding : MonoBehaviour
    {
        public GameObject wayObject;
        public GameObject openObject;
        public GameObject closeObject;
        public GameObject mapObject;
        
        public bool debug = false;
        
        public Transform debugTransform;

        int PathFindingCount => 32;
        
        void Awake()
        {
            if (debugTransform == null) debugTransform = transform;
        }

        void DebugPath(GameObject objectByType, Vector2 pos)
        {
            var path = Instantiate(objectByType, new Vector3(pos.x, 0, pos.y) * GameData.TileSize, Quaternion.identity);
            var originScale = objectByType.transform.localScale;
            path.transform.localScale = new Vector3(originScale.x, originScale.y, originScale.z) * GameData.TileSize;
        }

        public void PrintWay(List<TileNode> wayNodeList)
        {
            wayNodeList.ForEach(i => DebugPath(wayObject, i.tileCoordinate));
        }
        
        readonly Vector2[] _nearArray =
        {
            new(1, 0), new(-1, -1), new(0, -1), new(1, -1),
            new(-1, 1), new(0, 1), new(1, 1), new(-1, 0),
        };

        List<TileNode> GetNearList(TileNode currentNode, TileNode endNode, List<TileNode> map)
        {
            var nears = new List<TileNode>();

            foreach (var direction in _nearArray)
            {
               var nearCoordinate = currentNode.tileCoordinate + direction;
               
               nears.AddRange(map.
                   Where(tile => tile.tileCoordinate == nearCoordinate).
                   Where(tile => tile.tileCoordinate == endNode.tileCoordinate));
            }
            return nears;
        }

        List<TileNode> Retrace(TileNode endNode, TileNode startNode)
        {
            var path = new List<TileNode>();
            var currentNode = endNode;
            
            while (currentNode != null && startNode != null)
            {
                path.Add(currentNode);
                currentNode.tileNodeParent = currentNode;
            }
            
            path.Reverse();
            return path;
        }

        TileNode GetMin(List<TileNode> openList, TileNode endNode)
        {
            foreach (var node in openList)
            {
                node.farDistance = Vector2.Distance(node.tileCoordinate, endNode.tileCoordinate);
            }
            
            return openList.OrderBy(i => i.farDistance).First();
        }

        public List<TileNode> GreedyPath(List<TileNode> openList, TileNode endNode, List<TileNode> map)
        {
            print("there is no path");
            return null;
        }
        
        public List<TileNode> AstarPath(List<TileNode> openList, TileNode endNode, List<TileNode> map)
        {
            print("there is no path");
            return null;
        }
        
    }
}
