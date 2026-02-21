using System;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    [Serializable]
    public class ScoreData: ILoseModel
    {
        public event Action OnScoreChanged;

        [field: SerializeField] public int Score { get; private set; }
        
        private int _zeroCountScore = 0;
        private int _scoreForKill = 10;

        public void Reset()
        {
            Score = _zeroCountScore;
            OnScoreChanged?.Invoke();
        }

        public void AddScore()
        {
            Score += _scoreForKill;
            OnScoreChanged?.Invoke();
        }

        public void SaveResult(int score)
        {
            Score = score;
            OnScoreChanged?.Invoke();
        }
    }
}
