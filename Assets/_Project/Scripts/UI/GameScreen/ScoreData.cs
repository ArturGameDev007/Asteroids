using System;
using _Project.Scripts.Configs.Enemies;

namespace _Project.Scripts.UI.GameScreen
{
    public class ScoreData: ILoseModel
    {
        public event Action OnScoreChanged;

        public int Score { get; private set; }
        
        private int _zeroCountScore = 0;

        public void Reset()
        {
            Score = _zeroCountScore;
            OnScoreChanged?.Invoke();
        }

        public void AddScore(EnemyConfig  config)
        {
            Score += config.ScoreForKill;
            OnScoreChanged?.Invoke();
        }

        public void SaveResult(int score)
        {
            Score = score;
            OnScoreChanged?.Invoke();
        }
    }
}
