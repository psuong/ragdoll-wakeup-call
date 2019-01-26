using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [Serializable]
    public struct CameraAxis : IComponentData {
        public Vector2 Value;
    }

    public class CameraAxisComponent : ComponentDataWrapper<CameraAxis> { }

}