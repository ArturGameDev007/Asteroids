namespace _Project.Scripts.Services.Analytics
{
    public interface IAnalyticsService
    {
        public void LogGameStart();
        
        public void LogGameEnd(int amountShots, int amountUsedLaser, int amountDestroyedEnemies);
        
        public void LogLaserUsed();
    }
}