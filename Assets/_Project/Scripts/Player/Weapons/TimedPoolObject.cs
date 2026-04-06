using System;
using System.Threading;
using _Project.Scripts.Services.RemoteConfigs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class TimedPoolObject : MonoBehaviour
    {
        private IRemoteConfigs _remoteConfigs;

        private DirectionShot _directionShot;
        private CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public void Construct(IRemoteConfigs remoteConfigs)
        {
            _remoteConfigs = remoteConfigs;
        }

        private void Awake()
        {
            _directionShot = GetComponent<DirectionShot>();
        }

        private void OnEnable()
        {
            if (_directionShot != null)
                _directionShot.enabled = true;
            
            StopLifeTimer();
            
            _cancellationTokenSource = new CancellationTokenSource();
            
            ReturnRoutineAsync().Forget();
        }

        private void OnDisable()
        {
            StopLifeTimer();
        }

        public void StopLifeTimer()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = null;
            }
        }

        public void ForceReturn()
        {
            gameObject.SetActive(false);
            
            StopLifeTimer();
            ReturnToPool();
        }

        private async UniTaskVoid ReturnRoutineAsync()
        {
            try
            {
                int millisecondsDelay = 1000;
                int delay = (int)(_remoteConfigs.RemoteConfig.LifeTimeShoot * millisecondsDelay);
                
                await UniTask.Delay(delay, cancellationToken: _cancellationTokenSource.Token);
                
                ReturnToPool();
            }
            catch (OperationCanceledException){}
        }

        protected abstract void ReturnToPool();
    }
}