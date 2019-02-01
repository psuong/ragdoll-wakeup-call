using System;
using Unity.Entities;
using UnityEngine;

namespace RagdollWakeUp {
    [Serializable]
    public struct Timer : IComponentData {
        public float Value;

        public float Minutes => Mathf.Floor (Value / 60f);
    }

    public class TimerComponent : ComponentDataWrapper<Timer> { }
}