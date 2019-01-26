using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [Serializable]
    public struct CameraOrbit : IComponentData {
        public Vector3 Pivot;
        public float Distance;
        public float Height;
        public float OrbitSensitivity;
        public float OrbitSpeed;
    }

    public class CameraOrbitComponent : ComponentDataWrapper<CameraOrbit> { }
}