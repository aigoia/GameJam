using System;
using UnityEngine;

namespace Script.Grid
{
    public class TileNode : MonoBehaviour
    {
        public TileType tileType;
        public Vector2 tileCoordinate;
        public float farDistance;
        public TileNode tileNodeParent;

        public void Init(Vector2 coordinate, TileType type = TileType.Default)
        {
            tileCoordinate = coordinate;
            tileType = type;
        }

        void OnMouseDown()
        {
            Debug.Log($"Tile clicked at ({tileCoordinate.x}, {tileCoordinate.y})");
        }
    }

    public enum TileType
    {
        Default,
    }
}
