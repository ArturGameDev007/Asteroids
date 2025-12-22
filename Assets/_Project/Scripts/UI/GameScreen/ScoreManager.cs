using Assets.Scripts.Player.Weapons;
using System;
using UnityEngine;

namespace Assets.Scripts.UI.GameScreen
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }

        public event Action<int> OnScoreLoaded;

        [SerializeField] private Laser _laser;
        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private int _score = 0;

        private int _minCountScore = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _score = _minCountScore;
        }

        private void OnEnable()
        {
            _laser.OnHit += AddScore;
        }

        private void OnDisable()
        {
            _laser.OnHit -= AddScore;
        }

        public void AddScore()
        {
            _score += 10;
            OnScoreLoaded?.Invoke(_score);
        }

        public void GetScore()
        {
            _viewScore.OnShowInfoFinalScore(_score);
        }
    }
}
