using Unity.Entities;
using TMPro;

namespace RagdollWakeUp {

    [System.Serializable]
    public struct TextsInstance : ISharedComponentData {
        public TextMeshProUGUI[] Values;
    }

    public class TextsInstanceComponent : SharedComponentDataWrapper<TextsInstance> { } 
}
