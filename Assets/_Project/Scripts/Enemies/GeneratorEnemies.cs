using System.Collections;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [Header("ObjectPool Enemies")]
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private float _spawnOffset = 4.5f;

        [SerializeField] private EnemyManager _enemyManager;
        
        private float _positionX;
        private float _positionY;
        private float _delay = 3f;

        private Camera _camera;
        private Coroutine _coroutine;
        
        public void Initialize()
        {
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
            Vector2 spawnViewport = GetRandomPoint();

            var enemy = _pool.GetObject();
            
            enemy.transform.position = spawnViewport;
            enemy.gameObject.SetActive(true);
            
            if (enemy.TryGetComponent(out Enemy enemyDiedHandler))
            {
                enemyDiedHandler.Initialize(_pool, _enemyManager);
            }
            
            if (enemy.TryGetComponent(out AsteroidController asteroid))
            {
                asteroid.SetDirection(spawnViewport);
            }
        }

        private Vector2 GetRandomPoint()
        {
            float margin = _spawnOffset + 1.0f; 

            int side = Random.Range(0, 4);
            float t = Random.value;

            Vector3 viewportPoint = side switch
            {
                0 => new Vector3(-margin, t, 10),
                1 => new Vector3(1 + margin, t, 10),
                2 => new Vector3(t, -margin, 10),
                _ => new Vector3(t, 1 + margin, 10)
            };

            return _camera.ViewportToWorldPoint(viewportPoint);
        }
    }
}
