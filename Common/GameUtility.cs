using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Script.Common
{
    public class GameUtility : MonoBehaviour {
        
        public static void ResetObjects(Transform node, GameObject holder)
        {
            var child = node.GetChild(0);
            DestroyImmediate(child.gameObject);
            Instantiate(holder, Vector3.zero, Quaternion.identity, node);
        }

        public static readonly Vector3[] Directions = 
        {
            Vector3.forward, Vector3.back, Vector3.right, Vector3.left,
            // new Vector3(0, 0, 1), new Vector3(0, 0, -1), new Vector3(1, 0, 0), new Vector3(-1, 0, 0),
        };
    
        public static readonly Vector3[] EightDirections =
        {
            // Vector3.forward, Vector3.back, Vector3.right, Vector3.left,
            new Vector3(0, 0, 1), 
            new Vector3(0, 0, -1), 
            new Vector3(1, 0, 0), 
            new Vector3(-1, 0, 0),
            new Vector3(1, 0, 1), 
            new Vector3(1, 0, -1), 
            new Vector3(-1, 0, 1), 
            new Vector3(-1, 0, -1),
        };
        
        public static readonly Vector2[] NearArray =
        {
            new(1, 0), new(-1, -1), new(0, -1), new(1, -1),
            new(-1, 1), new(0, 1), new(1, 1), new(-1, 0),
        };
        

        static Vector2 Vector2Round (Vector2 inputVector) {

            return new Vector2(Mathf.Round (inputVector.x), Mathf.Round (inputVector.y));
        }

        static Vector3 Vector3Round (Vector3 inputVector) {

            return new Vector3(Mathf.Round (inputVector.x), Mathf.Round (inputVector.y), Mathf.Round(inputVector.z));
        }

        public static void ShuffleArray<T>(T[] array)
        {
            for (int index = 0; index < array.Length; ++index)
            {
                var random1 = UnityEngine.Random.Range (0, array.Length);
                var random2 = UnityEngine.Random.Range (0, array.Length);
 
                (array[random1], array[random2]) = (array[random2], array[random1]);
            }
        }

        public static Vector2 Coordinate(Vector3 pos) {

            return Vector2Round(new Vector2(pos.x, pos.z) * GameData.TileSize);
        }
    
        public static Vector3 CoordinateToTransform(Vector2 pos) {

            return Vector3Round(new Vector3(pos.x, 0, pos.y) * GameData.TileSize);
        }
 
        public static void ShuffleList<T> (List<T> list)
        {
            for (int index = 0; index < list.Count; ++index)
            {
                var random1 = UnityEngine.Random.Range(0, list.Count);
                var random2 = UnityEngine.Random.Range(0, list.Count);
 
                (list[random1], list[random2]) = (list[random2], list[random1]);
            }
        }

        public static float GetHeuristic(Vector2 a, Vector2 b)
        {
           return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
        }
    }
}
