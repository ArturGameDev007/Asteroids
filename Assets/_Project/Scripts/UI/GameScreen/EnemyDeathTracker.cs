using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class EnemyDeathTracker : MonoBehaviour
    {
        [SerializeField] private ScoreData _scoreData;
        [SerializeField] private EnemyManager _enemyManager;

        private void Start()
        {
            _enemyManager.OnEnemyKilled += HandleEnemyKilled;
        }

        private void OnDestroy()
        {
            _enemyManager.OnEnemyKilled -= HandleEnemyKilled;
        }

        private void HandleEnemyKilled(Enemy enemy)
        {
            _scoreData.AddScore();
        }
    }
}