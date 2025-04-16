using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Script.Common;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Script.Grid
{
    public class CharacterMove : MonoBehaviour
    {
        float MoveSpeed => 3;
        float TurnSpeed => 9;
        
        Character _character;
        
        void Awake()
        {
            _character = GetComponent<Character>();
        }

        public async Task GoUnit(List<TileNode> waypoints)
        {
            var way = MakeTransform(waypoints);
            await TaskGo(way);
        }

        async Task TaskGo(List<Vector3> way)
        {
            Vector3 previous = new Vector3();
            
            foreach (var destination in way)
            {
                await TaskTurn(previous, destination);
                previous = destination;
                await TaskMove(destination);
            }
        }

        async Task TaskTurn(Vector3 previous, Vector3 destination)
        {
            var difference = 360f;
            Vector3 direction = destination - previous;
            direction.Normalize();

            while (difference > GameData.Epsilon)
            {
                difference = Rotation(direction);
                await Task.Yield();
            }
        }

        float Rotation(Vector3 direction)
        {
            var lookRotation = Quaternion.LookRotation(direction);
            var angleDifference = Quaternion.Angle(transform.rotation, lookRotation);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * TurnSpeed);
            return angleDifference;
        }
        
        async Task TaskMove(Vector3 destination)
        {
            while (Vector3.Distance(transform.position, destination) > GameData.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, MoveSpeed * Time.deltaTime);
                await Task.Yield();
            }
            
            transform.position = destination;
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
        
        List<Vector3> MakeTransform(List<TileNode> way)
        {
            return way.Select(node => GameUtility.CoordinateToTransform(node.tileCoordinate)).ToList();
        }
        
    }
}
