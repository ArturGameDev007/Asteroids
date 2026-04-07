using System;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public class GenerateLaser : MonoBehaviour, ILaserState
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        private IRemoteConfigs _remoteConfigs;
        
        private bool _isReloading;
        
        public float ReloadTime => _remoteConfigs.RemoteConfig.PlayerConfig.ReloadTimeLaser;
        private int MaxAmmo => _remoteConfigs.RemoteConfig.PlayerConfig.MaxAmountLaser;
        
        public int CurrentAmmonLaser { get; private set; }

        [Inject]
        public void Construct(IRemoteConfigs remoteConfigs)
        {
            _remoteConfigs = remoteConfigs;
        }

        private void Start()
        {
            CurrentAmmonLaser = MaxAmmo;
            ShowInfo();

            OnReloadProgress?.Invoke(ReloadTime);
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

            float timer = ReloadTime;
            float targetTime = 0f;

            while (timer > targetTime)
            {
                timer -= Time.deltaTime;
                OnReloadProgress?.Invoke(timer);

                await UniTask.Yield();
            }

            CurrentAmmonLaser = MaxAmmo;
            ShowInfo();

            OnReloadProgress?.Invoke(ReloadTime);
            _isReloading = false;
        }

        private void ShowInfo()
        {
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
        }
    }
}