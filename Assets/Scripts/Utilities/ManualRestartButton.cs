using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualRestartButton : MonoBehaviour {
    [SerializeField] public string[] scenePaths = new string[] {
        "Assets/Scenes/SampleScene.unity"
    };
    void Update () {
        if (Input.GetKeyDown ("1")) {

            SceneManager.LoadScene (scenePaths[0]);
            Debug.Log ($"loading scene \"{scenePaths[0]}\"");

            for (int i = 1; i < scenePaths.Length; i++) {

                SceneManager.LoadSceneAsync (scenePaths[i], LoadSceneMode.Additive);
                Debug.Log ($"loading scene \"{scenePaths[i]}\"");
            }
        }
    }
}