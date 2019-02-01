using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [Serializable]
    public struct FreeCamMovement : IComponentData {
        public float MoveSpeed;
        public Vector2 RotationSpeeds;
        public Vector2 yRotClamps;
        public Vector4 PositionClamps;
        public Vector2 HeightClamps;
    }

    public class FreeCamMovementComponent : ComponentDataWrapper<FreeCamMovement> { }
}