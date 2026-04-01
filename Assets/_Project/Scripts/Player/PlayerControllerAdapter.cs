namespace _Project.Scripts.Player
{
    public class PlayerControllerAdapter : IControllable
    {
        private readonly IPlayerProvider _playerProvider;

        public PlayerControllerAdapter(IPlayerProvider playerController)
        {
            _playerProvider = playerController;
        }

        public void ResetState()
        {
            _playerProvider.MovableEntity?.ResetState();
        }

        public void EnableControl()
        {
            _playerProvider.MovableEntity?.SetPaused(false);
        }

        public void DisableControl()
        {
            _playerProvider.MovableEntity?.SetPaused(true);
        }

        public void StopPhysics()
        {
            _playerProvider.MovableEntity?.StopPhysics();
        }
    }
}