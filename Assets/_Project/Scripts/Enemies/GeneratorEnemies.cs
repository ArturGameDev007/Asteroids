using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        // [SerializeField] private ObjectPool _pool;

        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private float _spawnOffset = 2.0f;

        private ObjectPool _pool;
        private List<Enemy> _activeEnemies = new();

        private float _positionX;
        private float _positionY;
        private float _delay = 3f;

        private Camera _camera;
        private Coroutine _coroutine;

        public void Initialize(ObjectPool pool)
        {
            _pool = pool;
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
            foreach (var enemy in _activeEnemies)
            {
                if (enemy.TryGetComponent(out Enemy enemies))
                    enemies.enabled = false;
            }

            _activeEnemies.Clear();
        }

        private IEnumerator GeneratorEnemy(float delay)
        {
            var wait = new WaitForSeconds(delay);

            while (true)
            {
                Spawn();
                yield return wait;
            }
        }

        private void Spawn()
        {
            var enemy = _pool.GetObject();

            if (!_activeEnemies.Contains(enemy))
                _activeEnemies.Add(enemy);

            Vector2 spawnViewport = GetRandomPoint();
            enemy.transform.position = spawnViewport;
            enemy.gameObject.SetActive(true);

            if (enemy.TryGetComponent(out Enemy enemyDiedHandler))
            {
                enemyDiedHandler.Initialize(_pool, _enemyManager);
            }

            if (enemy.TryGetComponent(out AsteroidController asteroid))
            {
                asteroid.enabled = true;
                asteroid.SetDirection(spawnViewport);
            }

            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
            {
                flyingSaucerController.enabled = true;
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