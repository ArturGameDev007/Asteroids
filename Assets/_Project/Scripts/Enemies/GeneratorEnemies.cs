using System.Collections.Generic;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public abstract class GeneratorEnemies
    {
        private readonly IRemoteConfigs _remoteConfig;
        private readonly List<Enemy> _activeEnemies = new();
        private readonly IEnemyDeathListener _enemyManager;
        private readonly ObjectPool<Enemy> _pool;
        private readonly Camera _camera;
        
        private float _positionX;
        private float _positionY;

        private float _spawnTimer;
        private bool _isGameActive;

        protected GeneratorEnemies(IRemoteConfigs remoteConfig, ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager, Camera camera)
        {
            _remoteConfig = remoteConfig;
            _camera = camera;
            _pool = pool;
            _enemyManager = enemyManager;
        }

        public void Process(float  deltaTime)
        {
            float minTimerThreshold = 0f;
            
            if (!_isGameActive)
                return;
            
            _spawnTimer -= deltaTime;

            while (_spawnTimer <= minTimerThreshold)
            {
                SpawnEntity(_pool);
                _spawnTimer += _remoteConfig.RemoteConfig.Delay;
            }
        }

        public void StartSpawning()
        {
            _isGameActive = true;
        }

        public void StopSpawning()
        {
            _isGameActive = false;
        }

        public void StopAllEnemies()
        {
            _isGameActive = false;

            foreach (var enemy in _activeEnemies)
            {
                enemy.StopPhysics(true);
                enemy.gameObject.SetActive(false);
                _pool.ReturnPool(enemy);
            }
        
            _activeEnemies.Clear();
        }
        
        private void SpawnEntity(ObjectPool<Enemy>  pool)
        {
            var enemy = pool.GetObject();
            
            if (!_activeEnemies.Contains(enemy))
                _activeEnemies.Add(enemy);

            Vector2 spawnPosition = GetRandomPoint();
            
            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);
            enemy.Construct(pool, _enemyManager, _remoteConfig);
            
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

            float margin = _remoteConfig.RemoteConfig.SpawnOffset;
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