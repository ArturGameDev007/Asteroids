using System;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public event Action<Enemy> OnEnemyKilled;

        private void OnEnemyDeath(Enemy enemy)
        {
            OnEnemyKilled?.Invoke(enemy);
        }
    }
}