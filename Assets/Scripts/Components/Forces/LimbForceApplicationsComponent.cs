using System;
using Unity.Entities;

namespace RagdollWakeUp.Forces {
    [Serializable]
    public struct LimbForceApplications : IComponentData {
        public float ForceMultiplierL;
        public float ForceMultiplierR;
    }
    public class LimbForceApplicationsComponent : ComponentDataWrapper<LimbForceApplications> {

    }
}

