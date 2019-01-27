using UnityEngine;
using UnityEngine.SceneManagement;

namespace RagdollWakeUp.Utilities {

    public class ManualRestartButton : MonoBehaviour {

        public string[] scenePaths = new string[] {
            "Assets/Scenes/SampleScene.unity"
        };

        public KeyCode reloadButton = KeyCode.R;

        private void Update () {
            if (Input.GetKeyDown(reloadButton)) {
                SceneManager.LoadScene (scenePaths[0]);
#if UNITY_EDITOR
                Debug.Log ($"Loading scene \"{scenePaths[0]}\"");
#endif
                for (int i = 1; i < scenePaths.Length; i++) {
                    SceneManager.LoadSceneAsync (scenePaths[i], LoadSceneMode.Additive);
#if UNITY_EDITOR
                    Debug.Log ($"Loading scene \"{scenePaths[i]}\"");
#endif
                }
            }
        }
    }
}
