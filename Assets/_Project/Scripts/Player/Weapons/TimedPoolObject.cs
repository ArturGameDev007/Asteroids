using System.Collections;
using _Project.Scripts.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class TimedPoolObject : MonoBehaviour
    {
        // [SerializeField] private float _lifeTime = 2.5f;
        
        [FormerlySerializedAs("_shootsConfig")] [SerializeField] private ShootingConfig  shootingConfig;

        private Coroutine _coroutine;

        private void OnEnable()
        {
            StopLifeTimer();
            _coroutine = StartCoroutine(ReturnRoutine());
        }

        private void OnDisable()
        {
            StopLifeTimer();
        }

        public void StopLifeTimer()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator ReturnRoutine()
        {
            var wait = new WaitForSeconds(shootingConfig.LifeTime);
            yield return wait;
            
            ReturnToPool();
        }

        protected abstract void ReturnToPool();
    }
}