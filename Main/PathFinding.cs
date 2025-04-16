using System.Collections.Generic;
using System.Linq;
using Script.Common;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Main
{
	public enum PathFindingStyle
    {
    	In, Out,
    }
	
    public class PathFinding : MonoBehaviour
    {
	    public GameObject mapObject;
        public GameObject openObject;
		public GameObject closedObject;
		public GameObject wayObject;

		public bool debugMod = false;

		Transform _debugPath;
		
		int PathFindingCount => 32;

		void Awake()
		{
			if (_debugPath == null) _debugPath = transform.Find("Path");
			if (_debugPath == null) _debugPath = transform;
		}

		void DebugPath(GameObject typeByObject, Vector2 pos)
		{
			var debugObject =  Instantiate(typeByObject, new Vector3(pos.x, 0, pos.y) * GameData.TileSize, Quaternion.identity, _debugPath);
			var originScale = typeByObject.transform.localScale;
			debugObject.transform.localScale = new Vector3(originScale.x, originScale.y, originScale.z) * GameData.TileSize;
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

		List<TileNode> GetNearList(TileNode currentNode, TileNode endNode, List<TileNode> map, PathFindingStyle pathFindingStyle)
		{
			var neighbours = new List<TileNode>();

			foreach (var direction in _nearArray)
			{
				var nearCoordinate = currentNode.tileCoordinate + direction;

				neighbours.AddRange(map.
					Where(tile => tile.tileCoordinate == nearCoordinate).
					Where(tile => tile == endNode || pathFindingStyle == PathFindingStyle.Out || tile.moveAwayType != MoveAwayType.NonSetting
					)
				);
			}
			return neighbours;
		}

		TileNode GetMin(List<TileNode> openList, TileNode endNode)
		{
			foreach (var node in openList)
			{
				node.farFormTarget = Vector2.Distance(node.tileCoordinate, endNode.tileCoordinate);
			}

			return openList.OrderBy(tileNode => tileNode.farFormTarget).First();
		}

		List<TileNode> Retrace(TileNode startNode, TileNode endNode)
		{
			var path = new List<TileNode>();
			var current = endNode;

			while (current != null && current != startNode)
			{
				path.Add(current);
				current = current.tileNodeParent;
			}

			path.Reverse();
			return path;
		}

		public List<TileNode> GreedPathFinding(TileNode startNode, TileNode endNode, List<TileNode> map, PathFindingStyle pathFindingStyle = PathFindingStyle.In)
		{
			var openList = new List<TileNode> { startNode };
			var closedSet = new HashSet<TileNode>();
			int limit = PathFindingCount;
			
			while (limit-- > 0 && openList.Count > 0)
			{
				var current = GetMin(openList, endNode);
				openList.Remove(current);
				closedSet.Add(current);

				if (current == endNode)
				{
					var path = Retrace(startNode, endNode);

					// Out mode return only OneArea or TwoArea
					return pathFindingStyle == PathFindingStyle.Out ? path.Where(n => n.moveAwayType == MoveAwayType.OneArea || n.moveAwayType == MoveAwayType.TwoArea).ToList() : path;
				}

				foreach (var neighbor in GetNearList(current, endNode, map, pathFindingStyle))
				{
					if (closedSet.Contains(neighbor)) continue;
					if (openList.Contains(neighbor)) continue;
					
					neighbor.tileNodeParent = current;
					openList.Add(neighbor);
				}

				if (!debugMod) continue;
				map.ForEach(tileNode => DebugPath(mapObject, tileNode.tileCoordinate));
				openList.ForEach(tileNode => DebugPath(openObject, tileNode.tileCoordinate));
				closedSet.ToList().ForEach(tileNode => DebugPath(closedObject, tileNode.tileCoordinate));
			}

			Debug.Log("There is no path.");
			return null;
		}
		
		public List<TileNode> AStarPathFinding(TileNode startNode, TileNode endNode, List<TileNode> map, PathFindingStyle pathFindingStyle = PathFindingStyle.In)
		{
			var openList = new List<TileNode> { startNode };
			var closedSet = new HashSet<TileNode>();

			startNode.gCost = 0;
			startNode.hCost = Vector2.Distance(startNode.tileCoordinate, endNode.tileCoordinate);

			var limit = PathFindingCount;

			while (limit-- > 0 && openList.Count > 0)
			{
				var current = openList.OrderBy(tileNode => tileNode.fCost).ThenBy(n => n.hCost).First();

				if (current == endNode)
				{
					var path = Retrace(startNode, endNode);

					return pathFindingStyle == PathFindingStyle.Out ? path.Where(tileNode => tileNode.moveAwayType == MoveAwayType.OneArea || tileNode.moveAwayType == MoveAwayType.TwoArea).ToList() : path;
				}

				openList.Remove(current);
				closedSet.Add(current);

				foreach (var neighbor in GetNearList(current, endNode, map, pathFindingStyle))
				{
					if (closedSet.Contains(neighbor)) continue;

					var tentativeGCost = current.gCost + Vector2.Distance(current.tileCoordinate, neighbor.tileCoordinate);

					if (openList.Contains(neighbor) && !(tentativeGCost < neighbor.gCost)) continue;
					
					neighbor.gCost = tentativeGCost;
					neighbor.hCost = Vector2.Distance(neighbor.tileCoordinate, endNode.tileCoordinate);
					neighbor.tileNodeParent = current;

					if (!openList.Contains(neighbor)) openList.Add(neighbor);
				}

				if (!debugMod) continue;
				map.ForEach(tileNode => DebugPath(mapObject, tileNode.tileCoordinate));
				openList.ForEach(tileNode => DebugPath(openObject, tileNode.tileCoordinate));
				closedSet.ToList().ForEach(tileNode => DebugPath(closedObject, tileNode.tileCoordinate));
			}

			Debug.Log("There is no path.");
			return null;
		}
	}
}
