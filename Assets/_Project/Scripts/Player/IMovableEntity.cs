namespace _Project.Scripts.Player
{
    public interface IMovableEntity
    {
        public void SetPaused(bool paused);
        public void ResetState();
        public void StopPhysics();
    }
}