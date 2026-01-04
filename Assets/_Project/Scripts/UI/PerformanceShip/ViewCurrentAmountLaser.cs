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

        private void Start()
        {
            Subscribe();
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReloadProgress += OnShowRollbackLaser;
        }

        private void Unsubscribe()
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
