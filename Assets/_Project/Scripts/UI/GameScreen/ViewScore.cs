using Scripts.UI.GameScreen;
using TMPro;
using UnityEngine;

namespace Scripts.GameScreen
{
    public class ViewScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textScore;

        [SerializeField] private ScoreData _scoreData;

        private void Start()
        {
            _scoreData.OnScoreChanged += OnShowInfoFinalScore;

            OnShowInfoFinalScore(_scoreData.GetScore());
        }

        private void OnDestroy()
        {
            _scoreData.OnScoreChanged -= OnShowInfoFinalScore;
        }

        public void OnShowInfoFinalScore(int value)
        {
            _textScore.text = $"Score: {value.ToString()}";
        }
    }
}
