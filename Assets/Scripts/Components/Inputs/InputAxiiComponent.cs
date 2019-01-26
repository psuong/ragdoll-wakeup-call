using System;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp.Inputs {
    /// <summary>
    /// Represents the player's input joystick values
    /// </summary>
    [Serializable]
    public struct InputAxii : ISharedComponentData {
        public NativeArray<Vector2> Value;
    }

    public class InputAxiiComponent : SharedComponentDataWrapper<InputAxii> {

        private void Awake () {
            Value = new InputAxii { Value = new NativeArray<Vector2> (2, Allocator.Persistent) };
        }

        private void OnDestroy () {
            var arr = Value.Value;

            if (arr.IsCreated) {
                arr.Dispose ();
            }
        }
    }
}