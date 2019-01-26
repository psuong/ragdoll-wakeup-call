using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [Serializable]
    public struct OrbitCameraReference : ISharedComponentData {
        public Camera Value;

        public Transform Transform => Value.transform;
    }

    public class OrbitCameraReferenceComponent : SharedComponentDataWrapper<OrbitCameraReference> { }
}