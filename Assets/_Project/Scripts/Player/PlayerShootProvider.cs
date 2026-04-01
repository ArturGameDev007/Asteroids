using _Project.Scripts.Player.Weapons;

namespace _Project.Scripts.Player
{
    public class PlayerShootProvider : IShootable
    {
        private readonly IPlayerProvider _input;
        
        public PlayerShootProvider(IPlayerProvider input)
        {
            _input = input;
        }

        public void EnableControl()
        {
            _input?.InputPauseHandler.SetPause(false);
        }

        public void DisableControl()
        {
            _input?.InputPauseHandler.SetPause(true);
        }

        public void StopAllShoots()
        {
            _input?.InputPauseHandler.StopShoots();
        }
    }
}