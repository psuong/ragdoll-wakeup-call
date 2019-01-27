using RagdollWakeUp.UI;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.GameStates.Systems {

    public class IdleQueueReadySystem : ComponentSystem {

        private ComponentGroup queueGroup, uiGroup;

        protected override void OnCreateManager() {
            queueGroup = GetComponentGroup(typeof(IdleStateQueueInstance), typeof(GameStateInstance));
            uiGroup = GetComponentGroup(typeof(ImageInstance), typeof(BackgroundColour));
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

            if (gameStates[0].Value == GameState.Idle) {

                var backgrounds = uiGroup.GetComponentDataArray<BackgroundColour>();
                var images      = uiGroup.GetSharedComponentDataArray<ImageInstance>();
                var background  = backgrounds[0];
                var image       = images[0].Value;

                background.CurrentDuration += Time.deltaTime;
                var t                       = background.CurrentDuration / background.FadeDuration;
                image.fillAmount            = t;
                backgrounds[0]              = background;
                if (t >= 1) {
                    gameStates[0] = new GameStateInstance { Value = GameState.Gameplay };
                    background.CurrentDuration = 0f;
                    backgrounds[0] = background;
                }
            }
        }
    }
}
