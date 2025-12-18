using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _player;

        [SerializeField] private List<Enemy> _prefab;

        private Queue<Enemy> _pool;

        public IEnumerable<Enemy> PoolEnemy => _pool;

        private void Awake()
        {
            _pool = new Queue<Enemy>();
        }

        public Enemy GetObject()
        {
            if (_pool.Count == 0)
            {
                int indexEnemy = Random.Range(0, _prefab.Count);
                var createEnemy = Instantiate(_prefab[indexEnemy]);

                if (createEnemy.TryGetComponent(out FlyingSaurcersController enemy))
                    enemy.Construsct(_player);

                createEnemy.transform.parent = _container;

                return createEnemy;
            }

            return _pool.Dequeue();
        }

        public void PutObject(Enemy enemy)
        {
            _pool.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
        }

        public void Reset()
        {
            _pool.Clear();
        }
    }
}
