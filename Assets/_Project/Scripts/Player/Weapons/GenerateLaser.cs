using System;
using _Project.Scripts.Configs.Player;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Player.Weapons
{
    public class GenerateLaser : MonoBehaviour, ILaserState
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        [field: SerializeField] public LaserConfig LaserConfig { get; private set; }

        private bool _isReloading;

        public float ReloadTime => LaserConfig.ReloadTime;
        public int CurrentAmmonLaser { get; private set; }

        private void Start()
        {
            CurrentAmmonLaser = LaserConfig.MaxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(LaserConfig.ReloadTime);
        }

        public bool TrySpendAmmo()
        {
            int minCountLazer = 0;

            if (_isReloading || CurrentAmmonLaser <= 0)
                return false;

            CurrentAmmonLaser--;

            ShowInfo();

            if (CurrentAmmonLaser <= minCountLazer)
                ReloadLaserAsync().Forget();

            return true;
        }
        
        private async UniTaskVoid ReloadLaserAsync()
        {
            _isReloading = true;

            float timer = LaserConfig.ReloadTime;
            float targetTime = 0f;

            while (timer > targetTime)
            {
                timer -= Time.deltaTime;
                OnReloadProgress?.Invoke(timer);

                await UniTask.Yield();
            }

            CurrentAmmonLaser = LaserConfig.MaxAmountLaser;
            ShowInfo();

            OnReloadProgress?.Invoke(LaserConfig.ReloadTime);
            _isReloading = false;
        }

        private void ShowInfo()
        {
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
        }
    }
}