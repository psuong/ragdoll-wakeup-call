using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Inputs.Systems {
    
    /**
     * Head, Chest, Arm, Legs (1, 2, 3, 4) for the players.
     */
    public class InputAxiiControllerReadSystem : ComponentSystem {

        private ComponentGroup inputDevicesGroup, limbGroups;

        protected override void OnCreateManager() {
            inputDevicesGroup = GetComponentGroup(ComponentType.ReadOnly<PlayerDevicePoolInstance>());
            limbGroups = GetComponentGroup(typeof(InputAxiiComponent), typeof(IDComponent));
        }

        protected override void OnUpdate() {
            var inputDevices = inputDevicesGroup.GetSharedComponentDataArray<PlayerDevicePoolInstance>();

            if (inputDevices.Length != 1) {
#if UNITY_EDITOR 
                Debug.LogError($"Expected 1 PlayerDevicePoolInstance, but got {inputDevices.Length} instead!");
#endif
                return;
            }

            var limbIds = limbGroups.GetComponentDataArray<ID>();
            var axii = limbGroups.GetComponentDataArray<InputAxii>();

            var devices = inputDevices[0].Value.Devices;

            for (int i = 0; i < devices.Length; i++) {
                var device = devices[i];
                var lhs = device.LeftStick.Value;
                var rhs = device.RightStick.Value;

                axii[i] = new InputAxii {
                    LeftJoyStick  = lhs,
                    RightJoyStick = rhs
                };
                
                Debug.Log(limbIds[i].Value);
            }
        }
    }
}
