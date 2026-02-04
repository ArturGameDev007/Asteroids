using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class EnemyDeathTracker : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemy;
        [SerializeField] private ScoreData _scoreData;

        public void Initialize(ScoreData scoreData)
        {
            _scoreData = scoreData;
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