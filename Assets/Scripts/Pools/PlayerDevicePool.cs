using System.Collections.Generic;
using InControl;
using UnityEngine;

namespace RagdollWakeUp.Pools {

    public class PlayerDevicePool : MonoBehaviour {

        public InputDevice[] Devices => devices.ToArray();
        
        private List<InputDevice> devices;

        private void Start() {
#if UNITY_EDITOR
            Debug.Log($"Initializing the pool!");
#endif
            devices = new List<InputDevice>();
            InputManager.OnDeviceAttached += OnAddInputDevice;
            InputManager.OnDeviceDetached += OnRemoveInputDevice;

            foreach (var device in InputManager.Devices) {
#if UNITY_EDITOR
                Debug.Log($"Adding device: {device} as Player: {devices.Count + 1}");
#endif
                devices.Add(device);
            }
        }

        private void OnDisable() {
#if UNITY_EDITOR
            Debug.Log($"Clearing the pool!");
#endif
            devices.Clear();
            InputManager.OnDeviceAttached -= OnAddInputDevice;
            InputManager.OnDeviceDetached -= OnRemoveInputDevice;
        }

        private void OnAddInputDevice(InputDevice device) {
#if UNITY_EDITOR
            Debug.Log($"Attaching device: {device} as player: {devices.Count + 1}");
#endif
            devices.Add(device);
        }

        private void OnRemoveInputDevice(InputDevice device) {
#if UNITY_EDITOR
            Debug.Log($"Removing device: {device} which is player: {devices.IndexOf(device) + 1}");
#endif
            devices.Remove(device);
        }
    }
}
