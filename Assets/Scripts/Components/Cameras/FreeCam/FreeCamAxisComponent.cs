using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [Serializable]
    public struct FreeCamAxis : IComponentData {
        public Vector2 Value;
    }
    public class FreeCamAxisComponent : ComponentDataWrapper<FreeCamAxis> { }
}