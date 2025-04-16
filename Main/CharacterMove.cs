using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Script.Common;
using UnityEngine;

namespace Script.Main
{
    public class CharacterMove : MonoBehaviour
    {
        public float moveSpeed = 0.28f;
        public float turnSpeed = 0.028f;
        

        Character _character;
        

        void Awake()
        {
            if (_character == null) _character = GetComponent<Character>();
            _character.activeState = ActiveState.Moving;
        }

        public async Task IndicateUnit(List<TileNode> way, bool skill = false)
        {
            var transformList = MakeTransform(way);
            await IndicatorAsync(transformList, skill);
        }

        List<Vector3> MakeTransform(List<TileNode> way)
        {
            return way.Select(node => GameUtility.CoordinateToTransform(node.tileCoordinate)).ToList();
        }

        async Task IndicatorAsync(List<Vector3> way, bool skill)
        {
            var previous = new Vector3();
            
            foreach (var destination in way)
            { 
                await LookToAsync(previous, destination);
                previous = destination;
                await MoveToAsync(destination);
            }
            
            EndIndicator(skill);
        }

        async Task MoveToAsync(Vector3 destination)
        {
            _character.activeState = ActiveState.Moving;

            while (Vector3.Distance(transform.position, destination) > GameData.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.position = destination;
            _character.activeState = ActiveState.NotAnything;
        }
        
        async Task LookToAsync(Vector3 previous, Vector3 destination)
        {
            float differenceRotation = 360f;
            Vector3 direction = destination - previous;
            
            while (differenceRotation > GameData.Epsilon)
            {
                differenceRotation = Rotation(direction);
                await Task.Yield();
            }
        }
        
        float Rotation(Vector3 targetDirection)
        {
            if (targetDirection.magnitude < GameData.Epsilon) return 0;

            var lookDirection = targetDirection.normalized;
            var targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            float angleDifference = Quaternion.Angle(transform.rotation, targetRotation);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

            return angleDifference;
        } 


        void StartIndicator(bool skill)
        {
            _character.activeState = ActiveState.Moving;
        }

        void EndIndicator(bool skill)
        {
            _character.activeState = ActiveState.NotAnything;
        }
    }
}