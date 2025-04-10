using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Main
{
    public class TileManager : MonoBehaviour
    {
        public GameObject tilePrefab;
        public int width = 10;
        public int height = 10;
        public float spacing = 1f;
        public Vector2 initPosition = new Vector2(0, 0);

        MainData _mainData;
        
        void Awake()
        {
            _mainData = FindObjectOfType<GameManager>().GetComponent<MainData>();
        }

        [ContextMenu("Spawn Tile")]
        void SpawnTile()
        {
            ClearTile();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 position = new Vector3(x * spacing + initPosition.x, 0, y * spacing + initPosition.y);
                    GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                    TileNode tileNode = tile.GetComponent<TileNode>();
                    tileNode.tileType = TileType.Default;
                    tileNode.tileCoordinate = new Vector2(x, y);
                    tileNode.tileStyle = TileStyle.OneArea;
                    tile.name = $"Tile ({x}, {y})";
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
