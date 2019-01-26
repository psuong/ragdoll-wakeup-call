using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Forces {
    [Serializable]
    public struct PlayerLimbs : ISharedComponentData {
        public Rigidbody LeftLimb;
        public Rigidbody RightLimb;
    }
    public class PlayerLimbsComponent : SharedComponentDataWrapper<PlayerLimbs> {

    };
}