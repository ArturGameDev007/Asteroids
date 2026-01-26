using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class ScoreController : MonoBehaviour, IEnemy
    {
        [SerializeField] private ScoreData _scoreData;

        private void Start()
        {
            enemy.OnEnemyKilled += HandleEnemyKilled;
        }

        private void OnDestroy()
        {
            _enemy.OnEnemyKilled -= HandleEnemyKilled;
        }

        public void HandleEnemyKilled(Enemy enemy)
        {
            _scoreData.AddScore();
        }
    }
}