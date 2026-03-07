using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class EnemyDeathTracker
    {
        private ILoseModel _scoreData;
        private IEnemyDeathListener _enemy;

        [Inject]
        public EnemyDeathTracker(ILoseModel scoreData, IEnemyDeathListener enemy)
        {
            _scoreData = scoreData;
            _enemy = enemy;
            
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