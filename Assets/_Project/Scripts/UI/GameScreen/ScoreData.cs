using System;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    [Serializable]
    public class ScoreData
    {
        public event Action<int> OnScoreChanged;

        [field: SerializeField] public int GetScore { get;  private set; }

        private int _zeroCountScore = 0;
        private int _scoreForKill = 10;

        public void Reset()
        {
            GetScore = _zeroCountScore;
            OnScoreChanged?.Invoke(GetScore);
        }

        public void AddScore()
        {
            GetScore += _scoreForKill;
            OnScoreChanged?.Invoke(GetScore);
        }
    }
}
