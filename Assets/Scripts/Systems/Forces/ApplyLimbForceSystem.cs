using System.Collections;
using System.Collections.Generic;
using RagdollWakeUp.Inputs;
using RagdollWakeUp.Tags;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
namespace RagdollWakeUp.Forces {
    public class ApplyLimbForceSystem : ComponentSystem {
        private ComponentGroup limbGroup;
        protected override void OnCreateManager () {
            limbGroup = GetComponentGroup (
                ComponentType.ReadOnly<InputAxii> (),
                ComponentType.ReadOnly<LimbForceApplications> (),
                ComponentType.ReadOnly<PlayerLimbs> (),
                ComponentType.Subtractive<UseLocalLimbForce> ()
            );
        }
        protected override void OnUpdate () {
            var inputAxiiArray = limbGroup.GetComponentDataArray<InputAxii> ();
            var limbForceApplicationsArray = limbGroup.GetComponentDataArray<LimbForceApplications> ();
            var rigidBodyArray = limbGroup.GetSharedComponentDataArray<PlayerLimbs> ();

            int N = inputAxiiArray.Length;
            for (int i = 0; i < N; i++) {
                var axii = inputAxiiArray[i];
                var forceMultiplier = limbForceApplicationsArray[i].ForceMultiplier;
                var limbs = rigidBodyArray[i];

                var leftForceVec = new Vector3 (axii.LeftJoyStick.x, axii.LeftJoyStick.y, 0);
                var rightForceVec = new Vector3 (axii.RightJoyStick.x, axii.RightJoyStick.y, 0);

                leftForceVec *= forceMultiplier;
                rightForceVec *= forceMultiplier;

                if (limbs.LeftLimb != null && limbs.LeftLimb.transform.localPosition.y < 2f) {
                    limbs.LeftLimb?.AddForce (leftForceVec);
                }

                if (limbs.RightLimb != null && limbs.RightLimb.transform.localPosition.y < 2f)
                    limbs.RightLimb?.AddForce (rightForceVec);
            }
        }
    }
}
