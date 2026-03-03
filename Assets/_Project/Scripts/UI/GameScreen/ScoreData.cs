using System;
using _Project.Scripts.Configs.Enemies;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    [Serializable]
    public class ScoreData: ILoseModel
    {
        public event Action OnScoreChanged;

        [field: SerializeField] public int Score { get; private set; }
        
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
