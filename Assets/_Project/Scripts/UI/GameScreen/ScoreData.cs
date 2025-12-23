using System;
using UnityEngine;

namespace Assets._Project.Scripts.UI.GameScreen
{
    [CreateAssetMenu(fileName = "NewScoreData", menuName = "Game/Score data", order = 51)]
    public class ScoreData : ScriptableObject
    {
        public event Action<int> OnScoreChanged;

        [SerializeField] private int _currentScore;

        private int _zeroCountScore = 0;
        private int _scoreForKill = 10;

        public void Reset()
        {
            _currentScore = _zeroCountScore;
            //OnScoreChanged?.Invoke(_currentScore);
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
