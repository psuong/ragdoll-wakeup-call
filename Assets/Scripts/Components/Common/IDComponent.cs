using System;
using Unity.Entities;

namespace RagdollWakeUp {
    [Serializable]
    public struct ID : IComponentData {
        public short Value;
    }

    public class IDComponent : ComponentDataWrapper<ID> { }
}