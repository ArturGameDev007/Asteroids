using System;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : MonoBehaviour, IEnemyDeathListener
    {
        public event Action OnEnemyKilled;

        public void OnEnemyDeath()
        {
            OnEnemyKilled?.Invoke();
        }
    }
}