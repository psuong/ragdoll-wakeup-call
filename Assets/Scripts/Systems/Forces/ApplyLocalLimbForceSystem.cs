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
    public class ApplyLocalLimbForceSystem : ComponentSystem {
        private ComponentGroup limbGroup, chestGroup, gameGroup;
        const int headChestIdNumber = 0;
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

            gameGroup = GetComponentGroup (
                ComponentType.ReadOnly<GameStateInstance> ()
            );
        }
        private Vector3 RotateVectorAroundAxis (Vector3 vec, Vector3 axis, float angle) {
            return Quaternion.AngleAxis (angle, axis) * vec;
        }
        protected override void OnUpdate () {
            if (!CanRunSimulation (ref gameGroup)) { return; }

            var inputAxiiArray = limbGroup.GetComponentDataArray<InputAxii> ();
            var limbForceApplicationsArray = limbGroup.GetComponentDataArray<LimbForceApplications> ();
            var rigidBodyArray = limbGroup.GetSharedComponentDataArray<PlayerLimbs> ();
            var idArray = limbGroup.GetComponentDataArray<ID> ();

            // This array should be size 1
            if (chestGroup.CalculateLength () == 0) {
#if UNITY_EDITOR
                Debug.Log ("didn't find an object with chestTag");
#endif
                return;
            }
            // Forces are applied relative to where the chest is facing
            var chestTransformArray = chestGroup.GetComponentArray<Transform> ();
            var chestTransform = chestTransformArray[0].transform;
            var chestForward = chestTransform.forward;
            var chestUp = chestTransform.up;
            var chestRight = chestTransform.right;

            int N = inputAxiiArray.Length;
            for (int i = 0; i < N; i++) {
                var axii = inputAxiiArray[i];
                var forceMultiplierL = limbForceApplicationsArray[i].ForceMultiplierL;
                var forceMultiplierR = limbForceApplicationsArray[i].ForceMultiplierR;
                var limbs = rigidBodyArray[i];

                // these values will be overwritten...
                var leftForceVec = new Vector3 (axii.LeftJoyStick.x, axii.LeftJoyStick.y, 0);
                var rightForceVec = new Vector3 (axii.RightJoyStick.x, axii.RightJoyStick.y, 0);
                leftForceVec *= forceMultiplierL;
                rightForceVec *= forceMultiplierR;
                var rightTorqueVec = Vector3.zero;

                // Direction of force depends on what limb it is
                if (idArray[i].Value == headChestIdNumber) { // Head and chest (or just torso...)
                    leftForceVec = axii.LeftJoyStick.x * chestRight + axii.LeftJoyStick.y * chestForward;
                    leftForceVec *= forceMultiplierL;

                    // rightTorqueVec = axii.RightJoyStick.x * forceMultiplierR;
                    // rightTorqueVec *= forceMultiplierR;

                } else if (idArray[i].Value == armIdNumber) { // Arms
                    leftForceVec = axii.LeftJoyStick.x * chestForward + axii.LeftJoyStick.y * chestUp;
                    rightForceVec = -axii.RightJoyStick.x * chestForward + axii.RightJoyStick.y * chestUp;
                    leftForceVec *= forceMultiplierL;
                    rightForceVec *= forceMultiplierR;

                    if (leftForceVec.y < 0) leftForceVec.y *= 10;
                    if (rightForceVec.y < 0) rightForceVec.y *= 10;

                    // Debug.Log ($"Forward:{chestForward} Up:{chestUp} Left:{left} Right:{right} LeftForce:{leftForceVec} RightForce:{rightForceVec}");

                } else if (idArray[i].Value == legIdNumber) { // Legs
                    var down = -chestUp;
                    leftForceVec = axii.LeftJoyStick.x * chestRight + axii.LeftJoyStick.y * chestForward;
                    rightForceVec = axii.RightJoyStick.x * chestRight + axii.RightJoyStick.y * chestForward;
                    leftForceVec *= forceMultiplierL;
                    rightForceVec *= forceMultiplierR;

                    if (leftForceVec.y < 0) leftForceVec.y *= 10;
                    if (rightForceVec.y < 0) rightForceVec.y *= 10;
                }

                if (limbs.LeftLimb != null && limbs.LeftLimb.transform.localPosition.y < 2f)
                    limbs.LeftLimb?.AddForce (leftForceVec);

                if (idArray[i].Value == headChestIdNumber) { // Apply "torque" on the chest by adding force at an offset
                    limbs.RightLimb?.AddTorque (chestUp * axii.RightJoyStick.x * forceMultiplierR);

                } else if (limbs.RightLimb != null && limbs.RightLimb.transform.localPosition.y < 2f)
                    limbs.RightLimb?.AddForce (rightForceVec);
            }
        }

        private bool CanRunSimulation (ref ComponentGroup group) {
            var states = group.GetComponentDataArray<GameStateInstance> ();
            return states.Length > 0 ? states[0].Value == GameState.Gameplay : true;
        }
    }
}
