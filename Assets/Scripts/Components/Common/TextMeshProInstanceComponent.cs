using Unity.Entities;
using TMPro;

namespace RagdollWakeUp {

    [System.Serializable]
    public struct TextMeshProInstance : ISharedComponentData {
        public TextMeshProUGUI Value;
    }

    public class TextMeshProInstanceComponent : SharedComponentDataWrapper<TextMeshProInstance> { }
}
