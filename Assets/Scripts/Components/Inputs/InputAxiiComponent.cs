using System;
using Unity.Entities;
using Unity.Mathematics;

namespace RagdollWakeUp.Inputs {
    /// <summary>
    /// Represents the player's input joystick values
    /// </summary>
    [Serializable]
    public struct InputAxii : IComponentData {
        public float2 LeftJoyStick, RightJoyStick;
    }

    public class InputAxiiComponent : ComponentDataWrapper<InputAxii> { }
}
