using Unity.Entities;

namespace RagdollWakeUp.Tags {
    
    public struct LegTag : IComponentData { }

    public class LegTagComponent : ComponentDataWrapper<LegTag> { }
}
