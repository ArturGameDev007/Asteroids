using _Project.Scripts.Player.Weapons;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    [RequireComponent(typeof(GenerateLaser))]
    public class ViewCurrentAmountLaser : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;

        private GenerateLaser _laser;

        [Inject]
        public void Construct(GenerateLaser laser)
        {
            _laser = laser;
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
