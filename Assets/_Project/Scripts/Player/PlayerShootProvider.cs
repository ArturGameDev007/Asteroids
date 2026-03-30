using _Project.Scripts.Player.Weapons;

namespace _Project.Scripts.Player
{
    public class PlayerShootProvider : IShootable
    {
        // private readonly InputForShoot _input;
        
        // private readonly IInputPauseHandler _input;
        private readonly IPlayerProvider _input;
        
        public PlayerShootProvider(IPlayerProvider input)
        {
            _input = input;
        }

        public void EnableControl()
        {
            _input?.InputPauseHandler.SetPause(false);
            // _input?.SetPause(false);
        }

        public void DisableControl()
        {
            _input?.InputPauseHandler.SetPause(true);
            // _input?.SetPause(true);
        }

        public void StopAllShoots()
        {
            _input?.InputPauseHandler.StopShoots();
            
            // _input?.StopShoots();
        }
    }
}