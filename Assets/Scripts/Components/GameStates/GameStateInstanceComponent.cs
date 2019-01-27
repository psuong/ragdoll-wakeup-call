using Unity.Entities;

namespace RagdollWakeUp.GameStates {

    [System.Serializable]
    public struct GameStateInstance : IComponentData {
        public GameState Value;
    }

    public class GameStateInstanceComponent : ComponentDataWrapper<GameStateInstance> { }
}
