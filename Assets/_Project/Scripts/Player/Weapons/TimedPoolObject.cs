using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class TimedPoolObject : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 0.5f;

        private Coroutine _coroutine;
        private WaitForSeconds _wait;

        private void Awake()
        {
            _wait = new WaitForSeconds(_lifeTime);
        }

        private void OnDestroy()
        {
            StopLifeTimer();
        }
        
        protected void StartLifeTimer()
        {
            _coroutine = StartCoroutine(ReturnRoutine());   
        }

        protected void StopLifeTimer()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator ReturnRoutine()
        {
            yield return _wait;

            ReturnToPool();
        }

        protected abstract void ReturnToPool();
    }
}