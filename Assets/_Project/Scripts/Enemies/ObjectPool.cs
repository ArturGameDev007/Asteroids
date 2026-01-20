using System.Collections.Generic;
using _Project.Scripts.Player;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class ObjectPool : MonoBehaviour
    {
        [Header("Transform Objects")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Character _player;

        [Space(10)]
        [Header("List Enemies")]
        [SerializeField] private List<Enemy> _prefab;

        private Queue<Enemy> _pool;

        public void Initialize()
        {
            _pool = new Queue<Enemy>();
            _container = new GameObject("ContainerForEnemies");
        }

        public Enemy GetObject()
        {
            int numberZero = 0;
            int minCountPool = 0;

            if (_pool.Count == numberZero)
            {
                int indexEnemy = Random.Range(minCountPool, _prefab.Count);
                var createEnemy = Instantiate(_prefab[indexEnemy], _container.transform);

                if (createEnemy.TryGetComponent(out FlyingSaucerController enemy))
                    enemy.Construct(_player.transform);                
                
                return createEnemy;
            }

            return _pool.Dequeue();
        }

        public void PutObject(Enemy enemy)
        {
            _pool.Enqueue(enemy);
            enemy.gameObject.SetActive(false);
        }

        public void ClearPool()
        {
            _pool.Clear();
        }
    }
}
