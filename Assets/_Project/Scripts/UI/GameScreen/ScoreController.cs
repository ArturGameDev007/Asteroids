using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private ScoreData _scoreData;
        [SerializeField] private EnemyDiedHandler _enemyPool;

        private void Start()
        {
            SubscribeToAllEnemies();
        }

        private void OnDestroy()
        {
            UnsubscribeFromAllEnemies();
        }

        private void SubscribeToAllEnemies()
        {
            _enemyPool.OnEnemyKilled += HandleEnemyKilled;
        }

        private void UnsubscribeFromAllEnemies()
        {
            _enemyPool.OnEnemyKilled -= HandleEnemyKilled;
        }

        private void HandleEnemyKilled(Enemy enemy)
        {
            _scoreData.AddScore();
        }
    }
}