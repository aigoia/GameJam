using System;
using System.Collections.Generic;
using Script.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Main
{
    public class GridManager : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = 1f;
        public Vector2 initPosition = new Vector2(0, 0);

        public MainData mainData;
        
        void Awake()
        {
            mainData ??= FindObjectOfType<MainData>();
        }

        [ContextMenu("Spawn Tile")]
        void SpawnTile()
        {
            ClearTile();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x * spacing + initPosition.x, 0, y * spacing + initPosition.y) * GameData.TileSize;
                    GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    var originScale = tilePrefab.transform.localScale;
                    tile.transform.localScale = new Vector3(originScale.x, originScale.y, originScale.z) * GameData.TileSize;
                    
                    TileNode tileNode = tile.GetComponent<TileNode>();
                    tileNode.tileType = TileType.Default;
                    tileNode.tileCoordinate = new Vector2(x, y);
                    tileNode.moveAwayType = MoveAwayType.OneArea;
                    tile.name = $"Tile ({x}, {y})";
                    
                    mainData.currentTileList.Add(tileNode);
                }
            }
        }

        [ContextMenu("Clear Tile")]
        void ClearTile()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}
