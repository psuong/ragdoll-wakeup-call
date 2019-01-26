using System;
using Unity.Entities;

namespace RagdollWakeUp.Forces {
    [Serializable]
    public struct LimbForceApplications : IComponentData {
        public float ForceMultiplier;
    }
    public class LimbForceApplicationsComponent : ComponentDataWrapper<LimbForceApplications> {

    }
}