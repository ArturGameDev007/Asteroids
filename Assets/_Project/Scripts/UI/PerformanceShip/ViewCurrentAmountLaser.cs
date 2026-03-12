using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class ViewCurrentAmountLaser : MonoBehaviour, ILaserView
    {
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;
        
        public void SetAmmonText(string text)
        {
            _textlaser.text = text;
        }

        public void SetReloadText(string text)
        {
            _rollbackLasera.text = text;
        }
    }
}
