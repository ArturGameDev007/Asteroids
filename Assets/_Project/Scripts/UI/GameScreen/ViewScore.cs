using Assets._Project.Scripts.UI.GameScreen;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.GameScreen
{
    public class ViewScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private ScoreData _scoreData;

        private void OnEnable()
        {
            _scoreData.OnScoreChanged += OnShowInfoFinalScore;
        }

        private void OnDisable()
        {
            _scoreData.OnScoreChanged -= OnShowInfoFinalScore;
        }

        public void OnShowInfoFinalScore(int value)
        {
            value = _scoreData.GetScore();
            _textScore.text = "Score: " + value.ToString();
        }
    }
}
