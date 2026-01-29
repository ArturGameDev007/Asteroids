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
            if (!_pool.TryDequeue(out Enemy enemy))
                enemy = CreateEnemy();

            enemy.gameObject.SetActive(true);
            
            return enemy;
        }

        private Enemy CreateEnemy()
        {
            int minCountPool = 0;
            
            int indexEnemy = Random.Range(minCountPool, _prefab.Count);
            var enemy = Instantiate(_prefab[indexEnemy], _container.transform);

            if (enemy.TryGetComponent(out FlyingSaucerController saucer))
                saucer.Construct(_player.transform);

            return enemy;
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