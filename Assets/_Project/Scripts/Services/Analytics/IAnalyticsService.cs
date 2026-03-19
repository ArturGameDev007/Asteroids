namespace _Project.Scripts.Services.Analytics
{
    public interface IAnalyticsService
    {
        public void LogGameStart();
        
        public void LogGameEnd();
        
        public void LogLaserUsed();
    }
}