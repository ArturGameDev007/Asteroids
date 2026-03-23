using System;
using System.Threading;
using _Project.Scripts.Configs.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class TimedPoolObject : MonoBehaviour
    {
        [SerializeField] private ShootingConfig _shootingConfig;

        private CancellationTokenSource _cancellationTokenSource;

        private void OnEnable()
        {
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

        private async UniTaskVoid ReturnRoutineAsync()
        {
            try
            {
                int millisecondsDelay = 1000;
                int delay = (int)(_shootingConfig.LifeTime * millisecondsDelay);
                
                await UniTask.Delay(delay, cancellationToken: _cancellationTokenSource.Token);
                
                ReturnToPool();
            }
            catch (OperationCanceledException){}
        }

        protected abstract void ReturnToPool();
    }
}