using _Project.Scripts.Player.Weapons;

namespace _Project.Scripts.Player
{
    public class PlayerShootProvider : IShootable
    {
        private InputForShoot _input;

        public PlayerShootProvider(InputForShoot input)
        {
            _input = input;
        }

        public void EnableControl()
        {
            _input.SetPause(false);
        }

        public void DisableControl()
        {
            _input.SetPause(true);
        }

        public void StopAllShoots()
        {
            _input.StopShoots();
        }
    }
}