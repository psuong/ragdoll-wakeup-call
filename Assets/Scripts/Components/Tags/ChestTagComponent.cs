using Unity.Entities;

namespace RagdollWakeUp.Tags {
    
    public struct ChestTag : IComponentData { }

    public class ChestTagComponent : ComponentDataWrapper<ChestTag> { }
}
