using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.GameStates {
    public class UpdateGameTimeSystem : ComponentSystem {

        private ComponentGroup timeGroup;

        protected override void OnCreateManager () {
            timeGroup = GetComponentGroup (
                ComponentType.ReadOnly<GameStateInstance> (),
                typeof (Timer)
            );
        }

        protected override void OnUpdate () {
            var dt = Time.deltaTime;

            var states = timeGroup.GetComponentDataArray<GameStateInstance> ();
            var timers = timeGroup.GetComponentDataArray<Timer> ();

            for (var i = 0; i < states.Length; ++i) {
                if (states[i].Value == GameState.Gameplay) {
                    var timer = timers[i];
                    timers[i] = new Timer { Value = timer.Value + dt };
                }
            }
        }
    }
}