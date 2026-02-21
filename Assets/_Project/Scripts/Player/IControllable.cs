namespace _Project.Scripts.Player
{
    public interface IControllable
    {
        public bool IsPaused { get; set; }
        
        public void ResetState();
        public void EnableControl();
        public void DisableControl();
        public void StopPhysics();
    }
}