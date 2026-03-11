using _Project.Scripts.Player.Weapons;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class ViewCurrentAmountLaser : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;
        
        private GenerateLaser _laser;

        [Inject]
        public void Construct(GenerateLaser laser)
        {
            _laser = laser;
        }

        private void Start()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReloadProgress += OnShowRollbackLaser;

            OnShowInfoLaser(_laser.CurrentAmmonLaser);
            OnShowRollbackLaser(_laser.LaserConfig.ReloadTime);
        }

        private void OnDestroy()
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
