using UnityEngine;

namespace Script.Main
{
    public class TileNode : MonoBehaviour
    {
        public TileType TileType { get; set; }
        public Vector2 TileCoordinate { get; set; }

        public TileNode(TileType tileType, Vector2 tileCoordinate)
        {
            TileType = tileType;
            TileCoordinate = tileCoordinate;
        } 
        
        void OnMouseDown()
        {
            Debug.Log($"Tile clicked at ({TileCoordinate.x}, {TileCoordinate.y})");
        }
    }

    public enum TileType
    {
        Default,
    }
}
