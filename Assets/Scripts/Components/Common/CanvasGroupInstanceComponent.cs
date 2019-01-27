using UnityEngine;
using Unity.Entities;

namespace RagdollWakeUp {

    [System.Serializable]
    public struct CanvasGroupInstance : ISharedComponentData {
        public CanvasGroup Value;
    }

    public class CanvasGroupInstanceComponent : SharedComponentDataWrapper<CanvasGroupInstance> { }
}
