using Unity.Entities;
using UnityEngine.UI;

namespace RagdollWakeUp {

    [System.Serializable]
    public struct ImageInstance : ISharedComponentData {
        public Image Value;
    }

    public class ImageInstanceComponent : SharedComponentDataWrapper<ImageInstance> { }
}
