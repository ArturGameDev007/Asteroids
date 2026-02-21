using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class GenerateLaser : MonoBehaviour
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        [SerializeField] private int _maxAmountLaser = 20;
        [SerializeField] private float _reloadTime = 5f;

        public int CurrentAmmonLaser { get; private set; }
        public bool IsReloading { get; private set; }

        private void Start()
        {
            CurrentAmmonLaser = _maxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(_reloadTime);
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

            float timer = _reloadTime;
            float targetTime = 0f;

            while (timer > targetTime)
            {
                timer -= Time.deltaTime;
                OnReloadProgress?.Invoke(timer);
                
                yield return null;
            }

            CurrentAmmonLaser = _maxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(_reloadTime);
            IsReloading = false;
        }

        private void ShowInfo()
        {
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
        }
    }
}