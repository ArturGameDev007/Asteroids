using Scripts.Player.Weapons;
using TMPro;
using UnityEngine;

namespace Scripts.PerformanceShip
{
    public class ViewCurrentAmountLaser : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;

        [SerializeField] private GenerateLaser _laser;

        private void OnEnable()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReloadProgress += OnShowRollbackLaser;
        }

        private void OnDisable()
        {
            _laser.OnLaserChanged -= OnShowInfoLaser;
            _laser.OnReloadProgress -= OnShowRollbackLaser;
        }

        private void OnShowInfoLaser(int value)
        {
            _textlaser.text = $"Laser<br>Ammon: {value.ToString()}";
        }

        private void OnShowRollbackLaser(float time)
        {
            _rollbackLasera.text = $"Rollback Laser: {time.ToString("F1")}s";
        }
    }
}
