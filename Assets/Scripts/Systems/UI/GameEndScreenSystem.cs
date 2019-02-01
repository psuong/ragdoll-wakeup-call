using RagdollWakeUp.GameStates;
using RagdollWakeUp.GameStates.Systems;
using RagdollWakeUp.Utilities;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RagdollWakeUp.UI.Systems {

    [UpdateAfter (typeof (WinDetectionSystem))]
    public class GameEndScreenSystem : ComponentSystem {

        private ComponentGroup uiGroup, gameStateGroup, uiGlobalGroup;

        protected override void OnCreateManager () {
            gameStateGroup = GetComponentGroup (
                typeof (GameStateInstance),
                ComponentType.ReadOnly<EndMessageInstance> (),
                typeof (ManualRestartInstance),
                ComponentType.ReadOnly<Timer> ()
            );
            uiGroup = GetComponentGroup (typeof (ImageInstance), typeof (TextMeshProInstance),
                typeof (BackgroundColour));
        }

        protected override void OnUpdate () {
            var gameStates = gameStateGroup.GetComponentDataArray<GameStateInstance> ();

            if (gameStates.Length != 1) {
#if UNITY_EDITOR
                Debug.LogError ($"Expected 1 GameState, but got {gameStates.Length} instead!");
#endif
                return;
            }

            var images = uiGroup.GetSharedComponentDataArray<ImageInstance> ();
            var texts = uiGroup.GetSharedComponentDataArray<TextMeshProInstance> ();
            var backgrounds = uiGroup.GetComponentDataArray<BackgroundColour> ();
            var endMessages = gameStateGroup.GetSharedComponentDataArray<EndMessageInstance> ();
            var restartHelpers = gameStateGroup.GetSharedComponentDataArray<ManualRestartInstance> ();
            var timers = gameStateGroup.GetComponentDataArray<Timer> ();

            for (int i = 0; i < images.Length; i++) {
                var current = gameStates[i].Value;
                var image = images[i].Value;
                var text = texts[i].Value;
                var timer = timers[0];
                var background = backgrounds[0];
                var endMessage = endMessages[0];

                if (current == GameState.Win) {

                    text.text = string.Format (endMessage.Value, timer.Minutes, Mathf.RoundToInt (timer.Value - timer.Minutes * 60f));
                    var orig = backgrounds[0];
                    var currentTime = orig.CurrentDuration += Time.deltaTime;
                    backgrounds[0] = orig;
                    var t = orig.CurrentDuration / orig.FadeDuration;
                    image.color = Color.Lerp (orig.FadedIn, orig.FadedOut, t * background.Speed);

                    var helper = restartHelpers[0];
                    if (currentTime >= background.FadeDuration + helper.Wait) {
                        SceneManager.LoadSceneAsync (helper.SceneName, LoadSceneMode.Single);
                    }
                }
            }
        }
    }
}