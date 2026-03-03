using System;
using System.Collections;
using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class GenerateLaser : MonoBehaviour
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        [SerializeField] private LaserConfig _laserConfig;

        public int CurrentAmmonLaser { get; private set; }
        public bool IsReloading { get; private set; }

        private void Start()
        {
            CurrentAmmonLaser = _laserConfig.MaxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(_laserConfig.ReloadTime);
        }

        public bool TrySpendAmmo()
        {
            int minCountLazer = 0;

            if (IsReloading || CurrentAmmonLaser <= 0)
                return false;

            CurrentAmmonLaser--;

            ShowInfo();

            if (CurrentAmmonLaser <= minCountLazer)
                StartCoroutine(ReloadLaser());

            return true;
        }

        private IEnumerator ReloadLaser()
        {
            IsReloading = true;

            float timer = _laserConfig.ReloadTime;
            float targetTime = 0f;

            while (timer > targetTime)
            {
                timer -= Time.deltaTime;
                OnReloadProgress?.Invoke(timer);
                
                yield return null;
            }

            CurrentAmmonLaser = _laserConfig.MaxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(_laserConfig.ReloadTime);
            IsReloading = false;
        }

        private void ShowInfo()
        {
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
        }
    }
}