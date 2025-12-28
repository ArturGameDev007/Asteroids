using System;
using UnityEngine;

namespace Assets._Project.Scripts.UI.GameScreen
{
    public class ScoreData : MonoBehaviour
    {
        public event Action<int> OnScoreChanged;

        [SerializeField] private int _currentScore;

        private int _zeroCountScore = 0;
        private int _scoreForKill = 10;

        private void Start()
        {
            Reset();
        }

        public void Reset()
        {
            _currentScore = _zeroCountScore;
        }

        public void AddScore()
        {
            _currentScore += _scoreForKill;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public int GetScore()
        {
            return _currentScore;
        }
    }
}
