using System.Collections;
using System.Collections.Generic;
using RagdollWakeUp.Inputs;
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
                typeof (Rigidbody)
            );
        }
        protected override void OnUpdate () {
            var inputAxiiArray = limbGroup.GetComponentDataArray<InputAxii> ();
            var limbForceApplicationsArray = limbGroup.GetComponentDataArray<LimbForceApplications> ();
            var rigidBodyArray = limbGroup.GetComponentArray<Rigidbody> ();
            int N = inputAxiiArray.Length;
            for (int i = 0; i < N; i++) {
                var axii = inputAxiiArray[i];
                var forceMultiplier = limbForceApplicationsArray[i].ForceMultiplier;

                var forceVec = new Vector3 (axii.LeftJoyStick.x, axii.LeftJoyStick.y, 0);
                forceVec *= forceMultiplier;

                rigidBodyArray[i].AddForce (forceVec);
            }
        }
    }
}