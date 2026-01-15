using System.Collections;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [Header("ObjectPool Enemies")]
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private float _spawnOffset = 0.5f;

        private float _positionX;
        private float _positionY;

        private float _delay = 3f;

        private ScoreData _scoreData;
        private Camera _camera;
        private Coroutine _coroutine;

        public void Initialize(ScoreData scoreData)
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
            // float positionX = Random.Range(-_positionX, _positionX);
            // float positionY = Random.Range(-_positionY, _positionY);
            // Vector2 positionSpawn = new Vector2(positionX, positionY);
            
            //
            // var enemy = _pool.GetObject();
            //
            // if (enemy.TryGetComponent(out Enemy enemyComponent))
            // {
            //     enemyComponent.Construct(_scoreData);
            // }
            //
            // enemy.gameObject.SetActive(true);
            // enemy.transform.position = positionSpawn;
            
            // float distanceToCamera = Mathf.Abs(_camera.transform.position.z);
    
            Vector3 spawnViewport = GetRandomPoint();
            
            var enemy = _pool.GetObject();
            enemy.transform.position = spawnViewport;
            enemy.gameObject.SetActive(true);
            
            // Vector3 spawnPos = _camera.ViewportToWorldPoint(new Vector3(spawnViewport.x, spawnViewport.y, distanceToCamera));
            // spawnPos.z = 0;
            

            if (enemy.TryGetComponent(out Enemy enemyComponent))
            {
                enemyComponent.Construct(_scoreData);
                
                Vector3 screenCenter = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, -_camera.transform.position.z));
                screenCenter.z = 0;
                
                enemyComponent.SetDirection((screenCenter - spawnViewport).normalized);
            }

        }

        private Vector3 GetRandomPoint()
        {
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
                    return camPos + Vector3.up * (top + offsetY); // fallback
            }
        }
    }
}
