using Unity.Entities;

namespace RagdollWakeUp.GameStates {

    /// <summary>
    /// Stores whether each person is ready.
    /// </summary>
    [System.Serializable]
    public struct IdleStateQueueInstance : ISharedComponentData {
        public bool[] Values;
    }

    public class IdleStateQueueInstanceComponent : SharedComponentDataWrapper<IdleStateQueueInstance> { }
}
