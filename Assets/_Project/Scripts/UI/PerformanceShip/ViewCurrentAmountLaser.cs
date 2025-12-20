using Assets.Scripts.Player.Weapons;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.PerformanceShip
{
    public class ViewCurrentAmountLaser : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textlaser;
        [SerializeField] private TextMeshProUGUI _rollbackLasera;

        [SerializeField] private InputForShoot _laser;

        private void OnEnable()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReplenishedLaserOverTime += OnShowRallbackLaser;
        }

        private void OnDisable()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReplenishedLaserOverTime -= OnShowRallbackLaser;
        }

        private void OnShowInfoLaser(int value)
        {
            _textlaser.text = "Laser" + "<br>" + "Ammon: " + value.ToString();
        }

        private void OnShowRallbackLaser(float time)
        {
            _rollbackLasera.text = "Rollback Laser: " + time.ToString("F1") + "s";
        }
    }
}
