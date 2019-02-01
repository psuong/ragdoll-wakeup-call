using System;
using Unity.Entities;

namespace RagdollWakeUp {
    [Serializable]
    public struct Timer : IComponentData {
        public float Value;
    }

    public class TimerComponent : ComponentDataWrapper<Timer> { }
}