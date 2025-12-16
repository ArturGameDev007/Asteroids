using System;
using UnityEngine;

namespace Assets.Scripts.UI.GameScreen
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance;

        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private int _score = 0;

        public event Action<int> ScoreLoaded;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _score = 0;
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
