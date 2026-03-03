using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Enemies;

namespace _Project.Scripts.UI.GameScreen
{
    public class EnemyDeathTracker
    {
        private ScoreData _scoreData;

        private IEnemyDeathListener _enemy;

        public void Initialize(ScoreData scoreData, IEnemyDeathListener enemyManager)
        {
            _scoreData = scoreData;
            _enemy = enemyManager;
            
            _enemy.OnEnemyKilled += OnEnemyDied;
        }

        public void Dispose()
        {
            _enemy.OnEnemyKilled -= OnEnemyDied;
        }

        private void OnEnemyDied(EnemyConfig config)
        {
            _scoreData.AddScore(config);
        }
    }
}