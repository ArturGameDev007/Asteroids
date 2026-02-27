using System;

namespace _Project.Scripts.Enemies
{
    public interface IEnemyDeathListener
    {
        public event Action OnEnemyKilled;
        
        public void OnEnemyDeath();
    }
}