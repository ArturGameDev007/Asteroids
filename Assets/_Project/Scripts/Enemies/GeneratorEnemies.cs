using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [SerializeField] private EnemyManager _enemyManager;

        [SerializeField] private float _spawnOffset = 2.0f;

        private ObjectPool<Enemy> _asteroid;
        private ObjectPool<Enemy> _ufo;
        private List<Enemy> _activeEnemies = new();

        private float _positionX;
        private float _positionY;
        private float _delay = 3f;

        private Camera _camera;
        private Coroutine _coroutine;

        private Character _player;
        
        private bool _isGameActive;

        public void Initialize(ObjectPool<Enemy> asteroid,  ObjectPool<Enemy> ufo, Character player)
        {
            _asteroid = asteroid;
            _ufo = ufo;
            _player = player;
            
            _camera = Camera.main;
        }

        public void StartSpawning()
        {
            _coroutine = StartCoroutine(GeneratorEnemy(_delay));
        }

        public void StopSpawning()
        {
            StopCoroutine(_coroutine);
        }

        public void StopAllEnemies()
        {
            _isGameActive = false;

            foreach (var enemy in _activeEnemies)
                enemy.StopPhysics(true);
            
            _activeEnemies.Clear();
        }

        private IEnumerator GeneratorEnemy(float delay)
        {
            _isGameActive = true;
            
            var wait = new WaitForSeconds(delay);

            while (_isGameActive)
            {
                SpawnEntity(_asteroid);
                SpawnEntity(_ufo);
                
                yield return wait;
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

            if (enemy.TryGetComponent(out AsteroidController asteroid))
            {
                asteroid.enabled = true;
                asteroid.SetDirection(spawnPosition);
            }
            else if (enemy.TryGetComponent(out FlyingSaucerController ufo))
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