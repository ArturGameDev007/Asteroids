using System;
using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class EnemyDeathTracker: IInitializable, IDisposable
    {
        private readonly ILoseModel _scoreData;
        private readonly IEnemyDeathListener _enemy;

        public EnemyDeathTracker(ILoseModel scoreData, IEnemyDeathListener enemy)
        {
            _scoreData = scoreData;
            _enemy = enemy;
        }
        
        public void Initialize()
        {
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