using System;
using _Project.Scripts.Configs.Enemies;

namespace _Project.Scripts.UI.GameScreen
{
    public interface ILoseModel
    {
        public event Action OnScoreChanged;
        
        public int Score { get; }
        
        public void Reset();

        public void AddScore(EnemyConfig  config);
        
        public void SaveResult(int score);
    }
}