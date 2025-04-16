using UnityEngine;

namespace Script.Grid
{
    public class GridManager : MonoBehaviour
    {
        int GridX => 12;
        int GridY => 12;
        
        [SerializeField] GameObject tilePrefab;
        [SerializeField] GameObject tileParent;
        GameObject _thisTileParent;
        
        [ContextMenu("Make Grid")]
        public void MakeGrid()
        {
            RemoveGrid();
            _thisTileParent = Instantiate(tileParent, transform);
            
            for (int i = 0; i < GridX; i++)
            {
                for (int j = 0; j < GridY; j++)
                {
                    var tileNode = Instantiate(tilePrefab, _thisTileParent.transform);
                    tileNode.transform.localPosition = new Vector3(i, 0, j);
                    tileNode.name = i + "," + j;
                    tileNode.GetComponent<TileNode>().tileCoordinate = new Vector2(i, j);
                }
            }
        }
        
        [ContextMenu("Remove Grid")]
        public void RemoveGrid()
        {
            if (_thisTileParent != null) DestroyImmediate(_thisTileParent.gameObject);
        }
    }
}
