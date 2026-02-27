namespace _Project.Scripts.Player
{
    public interface IInputService
    {
        public float HorizontalInput { get;}
        public float VerticalInput { get;}

        public void UpdateHorizontalInput();

        public void UpdateVerticalInput();
    }
}