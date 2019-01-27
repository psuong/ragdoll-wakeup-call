using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RagdollWakeUp.GameStates.Systems {

    public class WinDetectionSystem : ComponentSystem {

        private ComponentGroup positionsGroup, distanceGroup, gameStateGroup;

        protected override void OnCreateManager() {
            positionsGroup = GetComponentGroup(ComponentType.ReadOnly<Position>());
            distanceGroup  = GetComponentGroup(ComponentType.ReadOnly<DistanceGoal>());
            gameStateGroup = GetComponentGroup(typeof(GameStateInstance));
        }

        protected override void OnUpdate(){
            var positions = positionsGroup.GetComponentDataArray<Position>();

            if (positions.Length != 2) {
#if UNITY_EDITOR
                Debug.LogError($"Expected 2 Positions, but got {positions.Length} instead!");
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


            var gameStates = gameStateGroup.GetComponentDataArray<GameStateInstance>();

            if (gameStates.Length != 1) {
#if UNITY_EDITOR
                Debug.LogError($"Expected 1 GameStateInstance, but got {gameStates.Length} instead!");
#endif
                return;
            }

            var current = positions[0].Value;
            var goal    = positions[1].Value;
            current.y = goal.y = 0;

            var distance = math.distance(current, goal);

            if (distance < minDistances[0].Value){
#if UNITY_EDITOR
                Debug.Log("You Win");
#endif
                // TODO: Set the state.
                gameStates[0] = new GameStateInstance { Value = GameState.Win };
            }
        }
    }
}
