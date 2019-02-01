using RagdollWakeUp.Inputs;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    public class UpdateFreeCamAxisSystem : ComponentSystem {

        private ComponentGroup cameraGroup;

        protected override void OnCreateManager () {
            cameraGroup = GetComponentGroup (
                ComponentType.ReadOnly<InputAxii> (),
                ComponentType.ReadOnly<FreeCamMovement> (),
                typeof (FreeCamAxis)
            );
        }

        protected override void OnUpdate () {
            var dt = Time.deltaTime;
            var axii = cameraGroup.GetComponentDataArray<InputAxii> ();
            var movements = cameraGroup.GetComponentDataArray<FreeCamMovement> ();
            var camAxii = cameraGroup.GetComponentDataArray<FreeCamAxis> ();

            for (var i = 0; i < axii.Length; ++i) {
                var inputAxis = axii[i].RightJoyStick;
                var movement = movements[i];
                var camAxis = camAxii[i].Value;

                camAxis.x = Mathf.Clamp (camAxis.x - inputAxis.y * movement.RotationSpeeds.x * dt, movement.yRotClamps.x, movement.yRotClamps.y);
                camAxis.y += inputAxis.x * movement.RotationSpeeds.y * dt;
                camAxii[i] = new FreeCamAxis { Value = camAxis };
            }
        }
    }
}