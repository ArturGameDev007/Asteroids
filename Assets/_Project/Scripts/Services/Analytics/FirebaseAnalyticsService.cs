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

        public async void Initialize()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync();
        }

        public void LogGameStart()
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
            Debug.Log("<color=green>[Analytics]</color> Start Game");
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
            Debug.Log(
                $"<color=green>Firebase Sent:</color> Shots: {amountShots}, Lasers: {amountUsedLaser}, Kills: {amountDestroyedEnemies}");
        }

        public void LogLaserUsed()
        {
            FirebaseAnalytics.LogEvent(EVENT_LASER_USED);
            Debug.Log("<color=cyan>[Analytics]</color> Laser Used Sent");
        }
    }
}