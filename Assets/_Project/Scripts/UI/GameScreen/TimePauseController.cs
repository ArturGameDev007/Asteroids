namespace _Project.Scripts.UI.GameScreen
{
    public class TimePauseController
    {
        public float DeltaTime { get; private set; } = 1f;

        public void SetPause(bool isPaused)
        {
            DeltaTime = isPaused ? 0f : 1f;
        }
    }
}