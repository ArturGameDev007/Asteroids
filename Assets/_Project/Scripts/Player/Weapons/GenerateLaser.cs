using System;
using System.Collections;
using _Project.Scripts.Configs.Player;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class GenerateLaser : MonoBehaviour
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        [SerializeField] private LaserConfig _laserConfig;

        private int _currentAmmonLaser;
        private bool _isReloading;

        private void Start()
        {
            _currentAmmonLaser = _laserConfig.MaxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(_laserConfig.ReloadTime);
        }

        public bool TrySpendAmmo()
        {
            int minCountLazer = 0;

            if (_isReloading || _currentAmmonLaser <= 0)
                return false;

            _currentAmmonLaser--;

            ShowInfo();

            if (_currentAmmonLaser <= minCountLazer)
                StartCoroutine(ReloadLaser());

            return true;
        }

        private IEnumerator ReloadLaser()
        {
            _isReloading = true;

            float timer = _laserConfig.ReloadTime;
            float targetTime = 0f;

            while (timer > targetTime)
            {
                timer -= Time.deltaTime;
                OnReloadProgress?.Invoke(timer);
                
                yield return null;
            }

            _currentAmmonLaser = _laserConfig.MaxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(_laserConfig.ReloadTime);
            _isReloading = false;
        }

        private void ShowInfo()
        {
            OnLaserChanged?.Invoke(_currentAmmonLaser);
        }
    }
}