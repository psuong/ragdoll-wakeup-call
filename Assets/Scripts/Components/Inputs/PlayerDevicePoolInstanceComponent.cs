using RagdollWakeUp.Pools;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Inputs {

    [System.Serializable]
    public struct PlayerDevicePoolInstance : ISharedComponentData {
        public PlayerDevicePool Value;
    }

    public class PlayerDevicePoolInstanceComponent : SharedComponentDataWrapper<PlayerDevicePoolInstance> {
        private void Start() {
            Value = new PlayerDevicePoolInstance {
                Value = GetComponent<PlayerDevicePool>()
            };
        }
    }
}
