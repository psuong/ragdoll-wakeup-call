using RagdollWakeUp.UI;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.GameStates.Systems {

    public class IdleQueueReadySystem : ComponentSystem {

        private ComponentGroup queueGroup, uiGroup;

        protected override void OnCreateManager() {
            queueGroup = GetComponentGroup(typeof(IdleStateQueueInstance), typeof(GameStateInstance));
            uiGroup = GetComponentGroup(typeof(ImageInstance), typeof(BackgroundColour), typeof(CanvasGroupInstance));
        }

        protected override void OnUpdate() {
            var queue      = queueGroup.GetSharedComponentDataArray<IdleStateQueueInstance>();
            var gameStates = queueGroup.GetComponentDataArray<GameStateInstance>();

            if (queue.Length != 1 || gameStates.Length != 1) {
#if UNITY_EDITOR
                Debug.LogError($"Expected 1 IdleStateQueueInstance, but got {queue.Length} instead!");
#endif
                return;
            }

            var readyQueue = queue[0].Values;

            // Cheap hack, we only run the system when we're in the idle state.
            if (gameStates[0].Value == GameState.Idle && IsEveryoneReady(ref readyQueue)) {
                var backgrounds      = uiGroup.GetComponentDataArray<BackgroundColour>();
                var images           = uiGroup.GetSharedComponentDataArray<ImageInstance>();
                var canvases         = uiGroup.GetSharedComponentDataArray<CanvasGroupInstance>();
                var background       = backgrounds[0];
                var image            = images[0].Value;
                var canvas           = canvases[0].Value;

                background.CurrentDuration += Time.deltaTime;
                var t                       = background.CurrentDuration / background.FadeDuration;
                backgrounds[0]              = background;
                image.color                 = Color.Lerp(background.FadedOut, background.FadedIn, t * background.Speed);
                canvas.alpha                = 1 - t;

                if (t >= 1) {
                    gameStates[0] = new GameStateInstance { Value = GameState.Gameplay };
                    background.CurrentDuration = 0f;
                    backgrounds[0] = background;
                }
            }
        }

        private bool IsEveryoneReady(ref bool[] readyQueue) {
            for (int i = 0; i < readyQueue.Length; i++) {
                if (!readyQueue[i]) { return false; }
            }

            return true;
        }
    }
}
