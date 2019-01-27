using RagdollWakeUp.GameStates;
using RagdollWakeUp.GameStates.Systems;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.UI.Systems {

    [UpdateAfter(typeof(WinDetectionSystem))]
    public class GameEndScreenSystem : ComponentSystem { 

        private ComponentGroup uiGroup, gameStateGroup, uiGlobalGroup;

        protected override void OnCreateManager() {
            gameStateGroup = GetComponentGroup(typeof(GameStateInstance), ComponentType.ReadOnly<EndMessageInstance>());
            uiGroup        = GetComponentGroup(typeof(ImageInstance), typeof(TextMeshProInstance), 
                typeof(BackgroundColour));
        }
     
        protected override void OnUpdate() {
            var gameStates  = gameStateGroup.GetComponentDataArray<GameStateInstance>();

            if (gameStates.Length != 1) {
#if UNITY_EDITOR
                Debug.LogError($"Expected 1 GameState, but got {gameStates.Length} instead!");
#endif
                return;
            }

            var images      = uiGroup.GetSharedComponentDataArray<ImageInstance>();
            var texts       = uiGroup.GetSharedComponentDataArray<TextMeshProInstance>();
            var backgrounds = uiGroup.GetComponentDataArray<BackgroundColour>();
            var endMessages = uiGroup.GetSharedComponentDataArray<EndMessageInstance>();

            for (int i = 0; i < images.Length; i++) {
                var current    = gameStates[i].Value;
                var image      = images[i].Value;
                var text       = texts[i].Value;
                var background = backgrounds[0];
                var endMessage = endMessages[0];

                if (current == GameState.Win) {

                    text.text             = endMessage.Value;
                    var orig              = backgrounds[0];
                    orig.CurrentDuration += Time.deltaTime;
                    backgrounds[0]        = orig;
                    var t                 = orig.CurrentDuration / orig.FadeDuration;
                    image.color           = Color.Lerp(orig.FadedOut, orig.FadedIn, t * background.Speed);
                }

                if (current == GameState.Gameplay) {
                    image.color = background.FadedOut;
                }
            }
        }
    }
}
