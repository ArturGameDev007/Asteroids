using System;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : MonoBehaviour, IEnemyDeathListener
    {
        public event Action<Enemy> OnEnemyKilled;

        public void OnEnemyDeath(Enemy enemy)
        {
            OnEnemyKilled?.Invoke(enemy);
        }
    }
}