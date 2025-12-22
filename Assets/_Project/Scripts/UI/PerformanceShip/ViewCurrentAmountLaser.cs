using Assets._Project.Scripts.Player.Weapons;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.PerformanceShip
{
    public class ViewCurrentAmountLaser : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;

        [SerializeField] private GenerateLaser _laser;

        private void OnEnable()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReloadProgress += OnShowRallbackLaser;
        }

        private void OnDisable()
        {
            _laser.OnLaserChanged -= OnShowInfoLaser;
            _laser.OnReloadProgress -= OnShowRallbackLaser;
        }

        private void OnShowInfoLaser(int value)
        {
            //_textlaser.text = "Laser" + "<br>" + "Ammon: " + value.ToString();
            _textlaser.text = $"Laser Ammon: {value.ToString()}";
        }

        private void OnShowRallbackLaser(float time)
        {
            //_rollbackLasera.text = "Rollback Laser: " + time.ToString("F1") + "s";
            _rollbackLasera.text = $"Rollback Laser: {time.ToString("F1")}s";
        }
    }
}
