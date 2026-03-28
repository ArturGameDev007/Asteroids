using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerControllerAdapter : IControllable
    {
        // private PlayerController _playerController;
        private readonly IMovableEntity _movableEntity;

        public PlayerControllerAdapter(IMovableEntity playerController)
        {
            _movableEntity = playerController;
        }

        public void ResetState()
        {
            _movableEntity?.ResetState();
        }

        public void EnableControl()
        {
            _movableEntity?.SetPaused(false);
        }
        
        public void DisableControl()
        {
            _movableEntity?.SetPaused(true);
        }

        public void StopPhysics()
        {
            _movableEntity?.StopPhysics();
        }
    }
}