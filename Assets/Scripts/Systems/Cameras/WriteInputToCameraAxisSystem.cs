using RagdollWakeUp.Inputs;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    public class WriteInputToCameraAxisSystem : ComponentSystem {

        private ComponentGroup inputGroup;

        protected override void OnCreateManager () {
            inputGroup = GetComponentGroup (
                ComponentType.ReadOnly<InputAxii> (),
                typeof (CameraOrbit),
                typeof (CameraAxis)
            );
        }

        protected override void OnUpdate () {
            var inputs = inputGroup.GetComponentDataArray<InputAxii> ();
            var cameraAxii = inputGroup.GetComponentDataArray<CameraAxis> ();
            var orbits = inputGroup.GetComponentDataArray<CameraOrbit> ();
            var dt = Time.deltaTime;

            for (var i = 0; i < inputs.Length; ++i) {
                var input = inputs[i];
                var cameraAxis = cameraAxii[i];
                var orbit = orbits[i];

                var axis = cameraAxis.Value;
                axis.x += input.LeftJoyStick.x * dt * orbit.OrbitSensitivity;
                cameraAxii[i] = new CameraAxis { Value = axis };

                orbit.Height = Mathf.Clamp (orbit.Height += input.RightJoyStick.y * dt * orbit.OrbitSensitivity,
                    orbit.HeightRange.x, orbit.HeightRange.y);
                orbits[i] = orbit;
            }
        }
    }
}