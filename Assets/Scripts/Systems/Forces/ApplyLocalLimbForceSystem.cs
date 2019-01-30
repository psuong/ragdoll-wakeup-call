using System.Collections;
using System.Collections.Generic;
using RagdollWakeUp.Inputs;
using RagdollWakeUp.Tags;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
namespace RagdollWakeUp.Forces {
    public class ApplyLocalLimbForceSystem : ComponentSystem {
        private ComponentGroup limbGroup, chestGroup;
        const int armIdNumber = 2;
        const int legIdNumber = 3;
        protected override void OnCreateManager () {
            limbGroup = GetComponentGroup (
                ComponentType.ReadOnly<InputAxii> (),
                ComponentType.ReadOnly<LimbForceApplications> (),
                ComponentType.ReadOnly<PlayerLimbs> (),
                ComponentType.ReadOnly<ID> (),
                ComponentType.ReadOnly<UseLocalLimbForce> ()
            );
            chestGroup = GetComponentGroup (
                ComponentType.ReadOnly<ChestTag> (),
                typeof (Transform)
            );
        }
        private Vector3 RotateVectorAroundAxis (Vector3 vec, Vector3 axis, float angle) {
            return Quaternion.AngleAxis (angle, axis) * vec;
        }
        protected override void OnUpdate () {
            var inputAxiiArray = limbGroup.GetComponentDataArray<InputAxii> ();
            var limbForceApplicationsArray = limbGroup.GetComponentDataArray<LimbForceApplications> ();
            var rigidBodyArray = limbGroup.GetSharedComponentDataArray<PlayerLimbs> ();
            var idArray = limbGroup.GetComponentDataArray<ID> ();

            // This array should be size 1
            if (chestGroup.CalculateLength () == 0) {
                Debug.Log ("didn't find an object with chestTag");
                return;
            }
            // Forces are applied relative to where the chest is facing
            var chestTransformArray = chestGroup.GetComponentArray<Transform> ();
            var chestTransform = chestTransformArray[0].transform;
            var chestForward = chestTransform.forward;
            var chestUp = chestTransform.up;
            var chestRight = chestTransform.up;

            int N = inputAxiiArray.Length;
            for (int i = 0; i < N; i++) {
                var axii = inputAxiiArray[i];
                var forceMultiplier = limbForceApplicationsArray[i].ForceMultiplier;
                var limbs = rigidBodyArray[i];

                var leftForceVec = new Vector3 (axii.LeftJoyStick.x, axii.LeftJoyStick.y, 0);
                var rightForceVec = new Vector3 (axii.RightJoyStick.x, axii.RightJoyStick.y, 0);

                leftForceVec *= forceMultiplier;
                rightForceVec *= forceMultiplier;

                // Direction of force depends on what limb it is
                if (idArray[i].Value == armIdNumber) { // Arms
                    var left = RotateVectorAroundAxis (chestForward, chestUp, 90);
                    var right = chestRight;
                    leftForceVec = Quaternion.FromToRotation (new Vector3 (0, 0, 1), left) * leftForceVec;
                    rightForceVec = Quaternion.FromToRotation (new Vector3 (0, 0, 1), right) * rightForceVec;
                } else if (idArray[i].Value == legIdNumber) { // Legs
                    var down = RotateVectorAroundAxis (chestForward, chestRight, -90);
                    var R = Quaternion.FromToRotation (new Vector3 (0, 0, 1), down);
                    leftForceVec = R * leftForceVec;
                    rightForceVec = R * rightForceVec;
                }

                if (limbs.LeftLimb != null && limbs.LeftLimb.transform.localPosition.y < 2f)
                    limbs.LeftLimb?.AddForce (leftForceVec);

                if (limbs.RightLimb != null && limbs.RightLimb.transform.localPosition.y < 2f)
                    limbs.RightLimb?.AddForce (rightForceVec);
            }
        }
    }
}
