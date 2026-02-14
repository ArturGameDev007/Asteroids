using System.Collections;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 0.5f;

        private ObjectPool<Laser> _pool;
        private Coroutine _returnCoroutine;
        
        public void Initialize(ObjectPool<Laser> pool)
        {
            _pool = pool;

            if (_returnCoroutine != null)
                StopCoroutine(_returnCoroutine);

            _returnCoroutine = StartCoroutine(ReturnAfterTime(_lifeTime));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy _))
            {
                ReturnToPool();
            }
        }

        private IEnumerator ReturnAfterTime(float delay)
        {
            var wait = new WaitForSeconds(delay);
            yield return  wait;
            
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (_returnCoroutine != null)
            {
                StopCoroutine(_returnCoroutine);
                _returnCoroutine = null;
            }

            _pool?.PutObject(this);
        }

        private void OnDisable()
        {
            if (_returnCoroutine != null)
                StopCoroutine(_returnCoroutine);
        }
    }
}
