using Firebase;
using Firebase.Analytics;

namespace _Project.Scripts.Services.Analytics
{
    public class FirebaseAnalyticsService:IAnalyticsService
    {
        private const string EVENT_LASER_USED = "Laser_Used";
        
        public FirebaseAnalyticsService()
        {
            Initialize();
        }

        private async void Initialize()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync();
        }

        public void LogGameStart()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
        }

        public void LogGameEnd()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
        }

        public void LogLaserUsed()
        {
            FirebaseAnalytics.LogEvent(EVENT_LASER_USED);
        }
    }
}