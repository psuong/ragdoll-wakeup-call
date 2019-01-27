using RagdollWakeUp.Inputs;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.GameStates.Systems {

    /// <summary>
    /// Queues all players during the Idle State.
    /// </summary>
    public class IdleQueueSystem : ComponentSystem {

        private ComponentGroup globalSettingsGroup, imageGroup;

        protected override void OnCreateManager() {
            globalSettingsGroup = GetComponentGroup(typeof(GameStateInstance), 
                typeof(PlayerDevicePoolInstance), typeof(IdleStateQueueInstance));
            imageGroup = GetComponentGroup(typeof(ImageColoursInstance), typeof(TextsInstance));
        }

        protected override void OnUpdate() {
            var gameStates      = globalSettingsGroup.GetComponentDataArray<GameStateInstance>();
            var playerDevices   = globalSettingsGroup.GetSharedComponentDataArray<PlayerDevicePoolInstance>();
            var idleStateQueues = globalSettingsGroup.GetSharedComponentDataArray<IdleStateQueueInstance>();
            var imageColours    = imageGroup.GetSharedComponentDataArray<ImageColoursInstance>();
            var texts           = imageGroup.GetSharedComponentDataArray<TextsInstance>();

            if (gameStates.Length != 1 || playerDevices.Length != 1 || idleStateQueues.Length != 1) {
#if UNITY_EDITOR
                Debug.LogError($"Expected 1 of GameStateInstance, PlayerDevicePoolInstance, IdleStateQueueInstance, " +
                    $"but got {gameStates.Length}, {playerDevices.Length}, {idleStateQueues.Length} instead!");
#endif
                return;
            }

            var devices       = playerDevices[0].Value.Devices;
            var queue         = idleStateQueues[0].Values;
            var instance      = imageColours[0];
            var readyMessages = texts[0].Values;

            for (int i = 0; i < devices.Length; i++) {
                var currentDevice = devices[i];

                // Ready up the player when we're in the Idle state and the player has pressed the A button.
                if (!queue[i] && gameStates[0].Value == GameState.Idle && 
                    currentDevice.Action1.WasPressed) {
                    queue[i] = true;
                    instance.Images[i].color = instance.Values[i];
                    readyMessages[i].text = "Ready!";
                }
            }
        }
    }
}
