using Unity.Entities;

namespace RagdollWakeUp.Tags {
    
    public struct ArmTag : IComponentData { }

    public class ArmTagComponent : ComponentDataWrapper<ArmTag> { }
}
