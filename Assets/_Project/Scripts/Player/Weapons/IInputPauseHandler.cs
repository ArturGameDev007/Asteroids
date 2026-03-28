namespace _Project.Scripts.Player.Weapons
{
    public interface IInputPauseHandler
    {
        public void SetPause(bool isPaused);
        public void StopShoots();
    }
}