namespace _Project.Scripts.Player
{
    public interface IMovable
    {
        public void Move(float verticalInput);
        public void Rotate(float horizontalInput);
        public void Cancel();
    }
}