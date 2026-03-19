namespace _Project.Scripts.Services.Analytics
{
    public class AnalyticsService
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsService(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public void SendGameStart()
        {
            _analyticsService.LogGameStart();
        }
        
        public void SendGameEnd(int amountShots, int amountUsedLaser, int amountDestroyedEnemies)
        {
            _analyticsService.LogGameEnd(amountShots, amountUsedLaser, amountDestroyedEnemies);
        }
        
        public void SendLaserUsed()
        {
            _analyticsService.LogLaserUsed();
        }
    }
}