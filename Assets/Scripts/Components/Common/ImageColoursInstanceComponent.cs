using UnityEngine;
using UnityEngine.UI;
using Unity.Entities;

namespace RagdollWakeUp {

    [System.Serializable]
    public struct ImageColoursInstance : ISharedComponentData {
        public Color[] Values;
        public Image[] Images;
    }

    public class ImageColoursInstanceComponent : SharedComponentDataWrapper<ImageColoursInstance> {} 
}
