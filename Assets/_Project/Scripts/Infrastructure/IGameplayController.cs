namespace _Project.Scripts.Infrastructure
{
    public interface IGameplayController
    {
        public void Update(float deltaTime);
        
        public void ContinueGame();
        
        public void StopGameplay();
    }
}