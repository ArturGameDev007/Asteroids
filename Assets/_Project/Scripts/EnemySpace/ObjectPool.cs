using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("Transform Objects")]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _player;

        [Space(10)]
        [Header("List Enemies")]
        [SerializeField] private List<Enemy> _prefab;

        private Queue<Enemy> _pool;

        public IEnumerable<Enemy> PoolEnemy => _pool;

        private void Awake()
        {
            _pool = new Queue<Enemy>();
        }

        public Enemy GetObject()
        {
            int numberZero = 0;
            int minCountPool = 0;

            if (_pool.Count == numberZero)
            {
                int indexEnemy = Random.Range(minCountPool, _prefab.Count);
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
