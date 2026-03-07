using System;
using _Project.Scripts.Configs.Enemies;

namespace _Project.Scripts.UI.GameScreen
{
    public interface ILoseModel
    {
        public event Action OnScoreChanged;
        
        public void Reset();
        
        public int Score { get; }

        public void AddScore(EnemyConfig  config);
        
        public void SaveResult(int score);
    }
}