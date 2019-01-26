using Unity.Entities;

namespace RagdollWakeUp.Tags {
    
    public struct UseLocalLimbForce : IComponentData { }

    public class UseLocalLimbForceComponent : ComponentDataWrapper<UseLocalLimbForce> { }
}
