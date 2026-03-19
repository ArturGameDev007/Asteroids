using Unity.VisualScripting;

namespace _Project.Scripts.Services.Analytics
{
    public class MenuStartLogger : IInitializable
    {
        private readonly AnalyticsService _analyticsService;

        public MenuStartLogger(AnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        public void Initialize()
        {
            _analyticsService.SendGameStart();
        }
    }
}