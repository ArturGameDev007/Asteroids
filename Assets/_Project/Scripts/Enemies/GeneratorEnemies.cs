using System.Collections;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [Header("ObjectPool Enemies")]
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private float _spawnOffset = 2.5f;

        private float _positionX;
        private float _positionY;

        private float _delay = 3f;

        private Camera _camera;
        private Coroutine _coroutine;
        
        private ScoreData _scoreData;

        public void Initialize(ScoreData  scoreData)
        {
            _scoreData = scoreData;
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
            
            // if (enemy.TryGetComponent(out Enemy enemyDiedHandler))
            // {
            //     enemyDiedHandler.Initialize(_pool);
            // }
            
            if (enemy.TryGetComponent(out AsteroidController asteroid))
            {
                asteroid.SetDirection(spawnViewport);
            }
        }

        private Vector2 GetRandomPoint()
        {
            // float h = _camera.orthographicSize;
            // float w = h * _camera.aspect;
            //
            // Vector3 camPos = _camera.transform.position;
            //
            // float x, y;
            // int side = Random.Range(0, 4);
            //
            // if (side < 2) // Слева или Справа
            // {
            //     x = (side == 0) ? -w - _spawnOffset : w + _spawnOffset;
            //     y = Random.Range(-h, h);
            // }
            // else // Снизу или Сверху
            // {
            //     x = Random.Range(-w, w);
            //     y = (side == 2) ? -h - _spawnOffset : h + _spawnOffset;
            // }
            //
            // return camPos + new Vector3(x, y, 0);
            
            float camHalfHeight = _camera.orthographicSize;
            float camHalfWidth = camHalfHeight * _camera.aspect;
            Vector3 camPos = _camera.transform.position;
            
            float left = camPos.x - camHalfWidth;
            float right = camPos.x + camHalfWidth;
            float bottom = camPos.y - camHalfHeight;
            float top = camPos.y + camHalfHeight;
            
            float offsetX = _spawnOffset;
            float offsetY = _spawnOffset;
            
            switch (Random.Range(0, 4))
            {
                case 0: // Слева
                    return new Vector3(left - offsetX, Random.Range(bottom, top), 0);
                case 1: // Справа
                    return new Vector3(right + offsetX, Random.Range(bottom, top), 0);
                case 2: // Снизу
                    return new Vector3(Random.Range(left, right), bottom - offsetY, 0);
                case 3: // Сверху
                    return new Vector3(Random.Range(left, right), top + offsetY, 0);
                default:
                    return camPos + Vector3.up * (top + offsetY);
            }
        }
    }
}
