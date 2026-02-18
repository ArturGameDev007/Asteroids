namespace _Project.Scripts.Player
{
    public interface IInputService
    {
        public float Horizontal { get; }
        public float Vertical { get; }
        public void Update();
    }
}