using System;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : IEnemyDeathListener
    {
        public event Action OnEnemyKilled;

        public void OnEnemyDeath()
        {
            OnEnemyKilled?.Invoke();
        }
    }
}