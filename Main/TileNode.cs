using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Main
{
    public class TileNode : MonoBehaviour
    {
        public float gCost; // real distance
        public float hCost; // heuristic distance 

        public TileType tileType;
        public Vector2 tileCoordinate;
        public TileNode tileNodeParent;

        public MoveAwayType moveAwayType;
        public float farFormTarget;
        public float fCost;

        public TileNode(TileType tileType, Vector2 tileCoordinate)
        {
            this.tileType = tileType;
            this.tileCoordinate = tileCoordinate;
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
    
    public enum MoveAwayType
    {
        NonSetting, OneArea, TwoArea
    }
}
