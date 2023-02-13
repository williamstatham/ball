namespace Game.Scripts.Systems.Movement
{
    public interface IPlayerMovementManager
    {
        void MovePlayer(float gameSpeed, float deltaTime);

        void Reset();
    }
}