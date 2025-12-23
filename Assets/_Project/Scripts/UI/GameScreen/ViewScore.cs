using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.GameScreen
{
    public class ViewScore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private ScoreManager _manager;

        private void OnEnable()
        {
            _manager.OnScoreChanged += OnShowInfoFinalScore;
        }

        private void OnDisable()
        {
            _manager.OnScoreChanged -= OnShowInfoFinalScore;
        }

        public void OnShowInfoFinalScore(int value)
        {
            _textScore.text = "Score: " + value.ToString();
        }
    }
}
