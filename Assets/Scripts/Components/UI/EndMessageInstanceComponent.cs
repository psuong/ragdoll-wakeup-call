using Unity.Entities;

namespace RagdollWakeUp.UI {

    [System.Serializable]
    public struct EndMessageInstance : ISharedComponentData {
        public string Value;
    }

    public class EndMessageInstanceComponent : SharedComponentDataWrapper<EndMessageInstance> { }
}
