using UnityEngine;
using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;

namespace RagdollWakeUp.GameStates.Systems {

    public class WinDetection : ComponentSystem {

        private ComponentGroup positionsGroup, distanceGroup;

        protected override void OnCreateManager() {
            positionsGroup = GetComponentGroup(ComponentType.ReadOnly<Position>());
            distanceGroup  = GetComponentGroup(ComponentType.ReadOnly<DistanceGoal>());
        }

        protected override void OnUpdate(){
            var positions = positionsGroup.GetComponentDataArray<Position>();

            if(positions.Length != 2) {
#if UNITY_EDITOR
                Debug.LogError($"Only 1 Player Position and 1 Goal Position allowed");
#endif
                return;
            }

            var minDistances = distanceGroup.GetComponentDataArray<DistanceGoal>();

            if (minDistances.Length != 1) {
#if UNITY_EDITOR
                Debug.LogError($"Only 1 Distance allowed!");
#endif
                return;
            }

            var current = positions[0].Value;
            var goal    = positions[1].Value;

            current.y = 0;
            goal.y = 0;

            var distance = math.distance(current, goal);

            if (distance < minDistances[0].Value){
#if UNITY_EDITOR
                Debug.Log("You Win");
#endif
                // TODO: Set the state.
            }
        }
    }
}
