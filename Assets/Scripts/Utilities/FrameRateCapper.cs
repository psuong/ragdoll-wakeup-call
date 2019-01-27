using UnityEngine;

namespace RagdollWakeUp.Utilities {

    public class FrameRateCapper : MonoBehaviour {
        [SerializeField] private FrameRate framerate;

        public enum FrameRate : int {
            FPS30 = 30,
            FPS50 = 50,
            FPS60 = 60,
            FPS75 = 75,
            FPS90 = 90,
            FPS120 = 120
        }

        private void Start() {
            Application.targetFrameRate = (int)framerate;
        }
    }
}
