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
        
        public void SendGameEnd()
        {
            _analyticsService.LogGameEnd();
        }
        
        public void SendLaserUsed()
        {
            _analyticsService.LogLaserUsed();
        }
    }
}