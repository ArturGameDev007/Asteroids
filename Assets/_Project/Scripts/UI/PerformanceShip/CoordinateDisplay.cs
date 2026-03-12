using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class CoordinateDisplay : MonoBehaviour, ICoordinateView
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _coordinateText;
        
        public void SetCoordinateText(string text)
        {
            _coordinateText.text = text;
        }
    }
}
