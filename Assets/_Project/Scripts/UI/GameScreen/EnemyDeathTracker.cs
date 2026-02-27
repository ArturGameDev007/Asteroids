using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class EnemyDeathTracker : MonoBehaviour
    {
        [SerializeField] private ScoreData _scoreData;

        private IEnemyDeathListener _enemy;

        public void Initialize(ScoreData scoreData, IEnemyDeathListener enemyManager)
        {
            _scoreData = scoreData;
            _enemy = enemyManager;
        }

        private void Start()
        {
            _enemy.OnEnemyKilled += OnEnemyDied;
        }

        private void OnDestroy()
        {
            _enemy.OnEnemyKilled -= OnEnemyDied;
        }

        private void OnEnemyDied()
        {
            _scoreData.AddScore();
        }
    }
}