using Scripts.UI.GameScreen;
using TMPro;
using UnityEngine;

namespace Scripts.GameScreen
{
    public class ViewScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textScore;

        private ScoreData _scoreData;

        public void Create(ScoreData scoreData)
        {
            _scoreData = scoreData;
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
