namespace _Project.Scripts.Player
{
    public class PlayerControllerAdapter : IControllable
    {
        private PlayerController _playerController;

        public PlayerControllerAdapter(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void ResetState()
        {
            _playerController.ResetState();
        }

        public void EnableControl()
        {
            _playerController.SetPaused(false);
        }
        
        public void DisableControl()
        {
            _playerController.SetPaused(true);
        }

        public void StopPhysics()
        {
            _playerController.StopPhysics();
        }
    }
}