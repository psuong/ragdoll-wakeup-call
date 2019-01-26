using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Cameras {
    [UpdateAfter (typeof (WriteInputToCameraAxisSystem))]
    public class OrbitCameraSystem : ComponentSystem {

        private ComponentGroup cameraGroup;

        protected override void OnCreateManager () {
            cameraGroup = GetComponentGroup (
                ComponentType.ReadOnly<CameraAxis> (),
                ComponentType.ReadOnly<CameraOrbit> (),
                ComponentType.ReadOnly<OrbitCameraReference> ()
            );
        }

        protected override void OnUpdate () {
            var axii = cameraGroup.GetComponentDataArray<CameraAxis> ();
            var orbits = cameraGroup.GetComponentDataArray<CameraOrbit> ();
            var camRefs = cameraGroup.GetSharedComponentDataArray<OrbitCameraReference> ();

            var dt = Time.deltaTime;

            for (var i = 0; i < axii.Length; ++i) {
                var axis = axii[i];
                var orbit = orbits[i];
                var cam = camRefs[i].Transform;

                var worldPivot = orbit.Pivot;
                worldPivot += new Vector3 {
                    x = Mathf.Cos (axis.Value.x) * orbit.Distance,
                    y = orbit.Height,
                    z = Mathf.Sin (axis.Value.x) * orbit.Distance
                };

                cam.position = Vector3.Lerp (cam.position, worldPivot, dt * orbit.OrbitSpeed);
                cam.LookAt (orbit.Pivot);
            }
        }

    }
}