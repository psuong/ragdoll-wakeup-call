using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.UI {

    [System.Serializable]
    public struct BackgroundColour : IComponentData {
        public Color32 FadedOut, FadedIn;
        public float FadeDuration, CurrentDuration, Speed;
    }

    public class BackgroundColourComponent : ComponentDataWrapper<BackgroundColour> { }
}
