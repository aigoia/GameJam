using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Script.Common
{
    public class Show : MonoBehaviour
    {
        // Print any object
        public static void Print(object @object)
        {
            if (@object == null)
            {
                print("null");
                return;
            }

            // Check if the object is a list
            if (@object is IEnumerable<object> enumerable)
            {
                foreach (var item in enumerable)
                {
                    print(item);
                }
            }
            else
            {
                // If not a list, just print the object directly
                print(@object);
            }
        }

        // Print a list of tuples
        public static void PrintList<T>(List<T> list)
        {
            print($"[{string.Join(", ", list)}]");
        }

        // Print a list of custom index
        public static void PrintIndex<TK, TV>(List<(TK, TV)> list)
        {
            foreach (var item in list) {
                print($"({item.Item1}, {item.Item2})");
            }
        }
    
        // Print a list of objects with their indices (like Python's enumerate)
        public static void PrintEnumeratedList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                print($"{i}: {list[i]}");
            }
        }

        // Print a formatted list of key-value pairs (like a dictionary)
        public static void PrintDictionary<TK, TV>(Dictionary<TK, TV> dictionary) where TK : notnull
        {
            foreach (var pair in dictionary)
            {
                print($"{pair.Key}: {pair.Value}");
            }
        }
    }

    public class Map : MonoBehaviour
    {
        public static int[][] AddBorder(int[][] map, int block = 1)
        {
            int rows = map.Length;
            int cols = map[0].Length;
        
            int newRows = rows + 2;
            int newCols = cols + 2;
        
            int[][] newMap = new int[newRows][];
        
            for (int i = 0; i < newRows; i++)
            {
                newMap[i] = new int[newCols];
            }
        
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    newMap[i + 1][j + 1] = map[i][j]; 
                }
            }
        
            for (int i = 0; i < newRows; i++)
            {
                newMap[i][0] = block; 
                newMap[i][newCols - 1] = block; 
            }

            for (int j = 0; j < newCols; j++)
            {
                newMap[0][j] = block; 
                newMap[newRows - 1][j] = block; 
            }

            return newMap;
        }
    
        public static List<(Vector2 Coordinate, float Heuristic)> CheckMovableGreed(Vector2 playerVector, int[][] map, Vector2 endVector)
        {
            var movableList = new List<(Vector2 coordinate, float heuristic)>();
            int rows = map.Length;
            int cols = map[0].Length;

            Vector2[] directions = GetDirections();

            foreach (var direction in directions)
            {
                Vector2 newPos = playerVector + direction;
                int newX = (int)newPos.X;
                int newY = (int)newPos.Y;

                if (newX < 0 || newX >= rows || newY < 0 || newY >= cols) continue;
                if (map[newX][newY] == 1) 
                {
                    movableList.Add((newPos, GetSquaredDistance(newPos, endVector)));
                }
            }

            return movableList;
        }

        public static Vector2[] GetDirections()
        {
            return new[]
            {
                new Vector2(-1, 0),
                new Vector2(1, 0),
                new Vector2(0, -1),
                new Vector2(0, 1)
            };
        }

        public static float GetSquaredDistance(Vector2 point1, Vector2 point2)
        {
            Vector2 distance = point2 - point1;
            return distance.LengthSquared();
        }
    
        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(T[] array, int r, int start)
        {
            if (r == 0)
            {
                yield return Array.Empty<T>();
                yield break;
            }

            for (int i = start; i <= array.Length - r; i++)
            {
                foreach (var combination in GetCombinations(array, r - 1, i + 1))
                {
                    yield return new T[] { array[i] }.Concat(combination);
                }
            }
        }
    }
}
