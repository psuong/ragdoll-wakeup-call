using System.Collections;
using System.Collections.Generic;
using RagdollWakeUp.GameStates;
using RagdollWakeUp.Inputs;
using RagdollWakeUp.Tags;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace RagdollWakeUp.Forces {
    [UpdateAfter (typeof (FixedUpdate))]
    public class ApplyLimbForceSystem : ComponentSystem {
        private ComponentGroup limbGroup, gameGroup;
        protected override void OnCreateManager () {
            limbGroup = GetComponentGroup (
                ComponentType.ReadOnly<InputAxii> (),
                ComponentType.ReadOnly<LimbForceApplications> (),
                ComponentType.ReadOnly<PlayerLimbs> (),
                ComponentType.Subtractive<UseLocalLimbForce> ()
            );

            gameGroup = GetComponentGroup (
                ComponentType.ReadOnly<GameStateInstance> ()
            );
        }
        protected override void OnUpdate () {
            if (!CanRunSimulation (ref gameGroup)) { return; }

            var inputAxiiArray = limbGroup.GetComponentDataArray<InputAxii> ();
            var limbForceApplicationsArray = limbGroup.GetComponentDataArray<LimbForceApplications> ();
            var rigidBodyArray = limbGroup.GetSharedComponentDataArray<PlayerLimbs> ();

            int N = inputAxiiArray.Length;
            for (int i = 0; i < N; i++) {
                var axii = inputAxiiArray[i];
                var forceMultiplierL = limbForceApplicationsArray[i].ForceMultiplierL;
                var forceMultiplierR = limbForceApplicationsArray[i].ForceMultiplierR;
                var limbs = rigidBodyArray[i];

                var leftForceVec = new Vector3 (axii.LeftJoyStick.x, axii.LeftJoyStick.y, 0);
                var rightForceVec = new Vector3 (axii.RightJoyStick.x, axii.RightJoyStick.y, 0);

                leftForceVec *= forceMultiplierL;
                rightForceVec *= forceMultiplierR;

                if (limbs.LeftLimb != null && limbs.LeftLimb.transform.localPosition.y < 2f) {
                    limbs.LeftLimb?.AddForce (leftForceVec);
                }

                if (limbs.RightLimb != null && limbs.RightLimb.transform.localPosition.y < 2f)
                    limbs.RightLimb?.AddForce (rightForceVec);
            }
        }

        private bool CanRunSimulation (ref ComponentGroup group) {
            var states = group.GetComponentDataArray<GameStateInstance> ();
            return states.Length > 0 ? states[0].Value == GameState.Gameplay : true;
        }
    }
}
