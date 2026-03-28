using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class CoordinateDisplay : MonoBehaviour, ICoordinateView, ILaserView
    {
        [Header("Coordinate")]
        [SerializeField] private TextMeshProUGUI _coordinateText;

        [Header("Laser UI")]
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;

        public void SetCoordinateText(string text)
        {
            _coordinateText.text = text;
        }

        public void SetAmmonText(string text)
        {
            if (_textlaser != null)
                _textlaser.text = text;
        }

        public void SetReloadText(string text)
        {
            if (_rollbackLasera != null)
                _rollbackLasera.text = text;
        }
    }
}