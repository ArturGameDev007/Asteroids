using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class TimedPoolObject : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 3.0f;

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
            var wait = new WaitForSeconds(_lifeTime);
            yield return wait;
            
            ReturnToPool();
        }

        protected abstract void ReturnToPool();
    }
}