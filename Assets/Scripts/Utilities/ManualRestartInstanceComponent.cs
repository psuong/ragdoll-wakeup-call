using Unity.Entities;

namespace RagdollWakeUp.Utilities {

    [System.Serializable]
    public struct ManualRestartInstance : ISharedComponentData { 
        public float Wait;
        public string SceneName;
    }

    public class ManualRestartInstanceComponent : SharedComponentDataWrapper<ManualRestartInstance> { }
}
