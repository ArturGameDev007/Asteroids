using System.Collections;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 0.5f;

        private ObjectPool<Laser> _pool;
        private Coroutine _coroutine;
        
        public void Initialize(ObjectPool<Laser> pool)
        {
            _pool = pool;

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(ReturnAfterTime(_lifeTime));
        }

        private IEnumerator ReturnAfterTime(float delay)
        {
            var wait = new WaitForSeconds(delay);
            yield return  wait;
            
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _pool?.PutObject(this);
        }

        private void OnDisable()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}
