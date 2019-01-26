using Unity.Entities;

namespace RagdollWakeUp {

    /// <summary>
    /// Use this as a boolean value.
    /// </summary>
    public struct ByteBool : IComponentData {
        public byte Value;

        public static implicit operator bool(ByteBool value) => value.Value > 0;
        public static implicit operator ByteBool(bool value) => new ByteBool { Value = value ? (byte)0b001 : (byte)0b000 };
    }
} 
