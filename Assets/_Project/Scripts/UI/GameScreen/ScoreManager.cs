using System;
using UnityEngine;

namespace Assets.Scripts.UI.GameScreen
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private int _score = 0;

        public event Action<int> ScoreLoaded;

        private int _minCountScore = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _score = _minCountScore;
        }

        public void AddScore(int value)
        {
            _score += value;
            ScoreLoaded?.Invoke(value);
        }

        public void GetScore()
        {
            _viewScore.OnShowInfoFinalScore(_score);
        }
    }
}
