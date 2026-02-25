using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Player;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [Header("Manager")]
        [SerializeField] private EnemyManager _enemyManager;

        [Header("Settings Delays Enemies")]
        [SerializeField] private float _asteroidDelay = 2f;
        [SerializeField] private float _ufoDelay = 4f;
        [SerializeField] private float _spawnOffset = 2.0f;

        private ObjectPool<Enemy> _asteroid;
        private ObjectPool<Enemy> _ufo;

        private List<Enemy> _activeEnemies = new();
        
        private Camera _camera;
        
        private Coroutine _asteroidCoroutine;
        private Coroutine _ufoCoroutine;
        
        private float _positionX;
        private float _positionY;

        private bool _isGameActive;
        
        private Character _player;

        public void Initialize(ObjectPool<Enemy> asteroid, ObjectPool<Enemy> ufo, Character player)
        {
            _asteroid = asteroid;
            _ufo = ufo;
            _player = player;

            _camera = Camera.main;
        }

        public void StartSpawning()
        {
            _isGameActive = true;
            
            _asteroidCoroutine = StartCoroutine(GeneratorEnemy(_asteroid, _asteroidDelay));
            _ufoCoroutine = StartCoroutine(GeneratorEnemy(_ufo, _ufoDelay));
        }

        public void StopSpawning()
        {
            _isGameActive = false;
            
            if (_asteroidCoroutine != null)
                StopCoroutine(_asteroidCoroutine);
            
            if (_asteroidCoroutine != null)
                StopCoroutine(_ufoCoroutine);
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

        private void SpawnEntity(ObjectPool<Enemy> pool)
        {
            if (pool == null) return;

            var enemy = pool.GetObject();

            if (!_activeEnemies.Contains(enemy))
                _activeEnemies.Add(enemy);

            Vector2 spawnPosition = GetRandomPoint();
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);

            enemy.Initialize(pool, _enemyManager);
            enemy.StopPhysics(false);

            if (enemy is AsteroidController asteroid)
            {
                asteroid.enabled = true;
                asteroid.SetDirection(spawnPosition);
            }
            else if (enemy is FlyingSaucerController ufo)
            {
                ufo.enabled = true;
                ufo.Construct(_player.transform);
            }
        }

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