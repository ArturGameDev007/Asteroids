using System;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    [Serializable]
    public class ScoreData
    {
        public event Action<int> OnScoreChanged;

        [SerializeField] private int _currentScore;

        private int _zeroCountScore = 0;
        private int _scoreForKill = 10;

        public int GetScore { get; set; }

        public void Reset()
        {
            _currentScore = _zeroCountScore;
        }

        public void AddScore()
        {
            _currentScore += _scoreForKill;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }
}
