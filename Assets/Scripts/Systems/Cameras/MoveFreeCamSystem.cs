using RagdollWakeUp.Inputs;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [UpdateAfter (typeof (UpdateFreeCamAxisSystem))]
    public class MoveFreeCamSystem : ComponentSystem {
        private ComponentGroup cameraGroup;

        protected override void OnCreateManager () {
            cameraGroup = GetComponentGroup (
                ComponentType.ReadOnly<InputAxii> (),
                ComponentType.ReadOnly<FreeCamMovement> (),
                ComponentType.ReadOnly<OrbitCameraReference> (),
                typeof (FreeCamAxis)
            );
        }

        protected override void OnUpdate () {
            var dt = Time.deltaTime;
            var inputAxii = cameraGroup.GetComponentDataArray<InputAxii> ();
            var movements = cameraGroup.GetComponentDataArray<FreeCamMovement> ();
            var cams = cameraGroup.GetSharedComponentDataArray<OrbitCameraReference> ();
            var camAxii = cameraGroup.GetComponentDataArray<FreeCamAxis> ();

            for (var i = 0; i < cams.Length; ++i) {
                var input = inputAxii[i];
                var movement = movements[i];
                var transform = cams[i].Value.transform;
                var axis = camAxii[i];

                transform.localEulerAngles = new Vector3 (axis.Value.x, axis.Value.y, 0f);
                var fwd = transform.forward * input.LeftJoyStick.y * dt;
                var right = transform.right * input.LeftJoyStick.x * dt;
                var targetPos = transform.position + fwd + right;

                // Clamp the target positions
                targetPos.x = Mathf.Clamp (targetPos.x, movement.PositionClamps.x, movement.PositionClamps.y);
                targetPos.z = Mathf.Clamp (targetPos.z, movement.PositionClamps.z, movement.PositionClamps.w);
                targetPos.y = Mathf.Clamp (targetPos.y, movement.HeightClamps.x, movement.HeightClamps.y);

                transform.position = targetPos;
            }
        }
    }
}