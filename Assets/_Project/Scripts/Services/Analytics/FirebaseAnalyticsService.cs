using Firebase;
using Firebase.Analytics;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Analytics
{
    public class FirebaseAnalyticsService : IAnalyticsService, IInitializable
    {
        private const string PARAMETR_SHOTS = "Amount_Shots";
        private const string PARAMETR_USED_LASER = "Amount_Used_Laser";
        private const string PARAMETR_DESTROYED_ENEMIES = "Amount_Destroyed_Enemies";
        private const string EVENT_LASER_USED = "Laser_Used";

        private bool _wasUsedLaser;

        public void Initialize()
        {
            var task =  FirebaseApp.CheckAndFixDependenciesAsync();
            
            task.GetAwaiter().GetResult();
            
            Debug.Log("Firebase initialized");
        }

        public void LogGameStart()
        {
            _wasUsedLaser = false;
            
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
            
            Debug.Log("[Analytics]: Start Game.");
        }

        public void LogGameEnd(int amountShots, int amountUsedLaser, int amountDestroyedEnemies)
        {
            Parameter[] parameters =
            {
                new Parameter(PARAMETR_SHOTS, amountShots),
                new Parameter(PARAMETR_USED_LASER, amountUsedLaser),
                new Parameter(PARAMETR_DESTROYED_ENEMIES, amountDestroyedEnemies)
            };

            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd, parameters);
            Debug.Log($"[Analytics]: Shots: {amountShots}, Lasers: {amountUsedLaser}, Destroyed: {amountDestroyedEnemies}.");
        }

        public void LogLaserUsed()
        {
            if (_wasUsedLaser)
                return;
            
            FirebaseAnalytics.LogEvent(EVENT_LASER_USED);
            Debug.Log("[Analytics]: Laser Used.");
            
            _wasUsedLaser = true;
        }
    }
}