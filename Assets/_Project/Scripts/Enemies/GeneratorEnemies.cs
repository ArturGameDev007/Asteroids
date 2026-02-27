using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public abstract class GeneratorEnemies : MonoBehaviour
    {
        [Header("Settings Delays Enemies")]
        [SerializeField] private float _spawnOffset = 2.0f;
        [SerializeField] private float _delay = 3f;

        private List<Enemy> _activeEnemies = new();
        
        private IEnemyDeathListener _enemyManager;
        private ObjectPool<Enemy> _pool;
        
        private Camera _camera;
        private Coroutine _spawnCoroutine;
        
        private float _positionX;
        private float _positionY;

        private bool _isGameActive;
        
        public void Initialize(ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager)
        {
            _camera = Camera.main;
            _pool = pool;
            _enemyManager = enemyManager;
        }

        public void StartSpawning()
        {
            _isGameActive = true;

            _spawnCoroutine = StartCoroutine(GeneratorEnemy(_pool,  _delay));
        }

        public void StopSpawning()
        {
            _isGameActive = false;
            
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);
        }

        public void StopAllEnemies()
        {
            foreach (var enemy in _activeEnemies)
                enemy.StopPhysics(true);
        
            _activeEnemies.Clear();
        }
        
        private IEnumerator GeneratorEnemy(ObjectPool<Enemy> pool, float delay)
        {
            _isGameActive = true;

            var wait = new WaitForSeconds(delay);

            while (_isGameActive)
            {
                yield return wait;
                
                SpawnEntity(pool);
            }
        }

        private void SpawnEntity(ObjectPool<Enemy>  pool)
        {
            var enemy = pool.GetObject();
            
            if (!_activeEnemies.Contains(enemy))
                _activeEnemies.Add(enemy);

            Vector2 spawnPosition = GetRandomPoint();
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
            
            enemy.Initialize(pool, _enemyManager);
            
            ConfigureSpawn(enemy, spawnPosition);
            
            enemy.StopPhysics(false);
        }
        
        protected abstract void ConfigureSpawn(Enemy enemy, Vector2 spawnPosition);

        private Vector2 GetRandomPoint()
        {
            const int LeftSide = 0;
            const int RightSide = 1;
            const int BottomSide = 2;
            const int TopSide = 3;

            float ViewportMin = 0f;
            float ViewportMax = 1f;
            float CameraDistanceZ = 10f;

            float margin = _spawnOffset;
            float randomPositionAlongSide = Random.value;

            Vector3 viewportPoint;

            int side = Random.Range(0, 4);

            switch (side)
            {
                case LeftSide:
                    viewportPoint = new Vector3(ViewportMin - margin, randomPositionAlongSide, CameraDistanceZ);
                    break;

                case RightSide:
                    viewportPoint = new Vector3(ViewportMax + margin, randomPositionAlongSide, CameraDistanceZ);
                    break;

                case BottomSide:
                    viewportPoint = new Vector3(randomPositionAlongSide, ViewportMin - margin, CameraDistanceZ);
                    break;

                case TopSide:
                    viewportPoint = new Vector3(randomPositionAlongSide, ViewportMax + margin, CameraDistanceZ);
                    break;

                default:
                    viewportPoint = Vector3.zero;
                    break;
            }

            Vector2 worldPoint = _camera.ViewportToWorldPoint(viewportPoint);

            return worldPoint;
        }
    }
}