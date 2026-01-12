using System.Collections;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [Header("ObjectPool Enemies")]
        [SerializeField] private ObjectPool _pool;

        private float _positionX;
        private float _positionY;
        private float _spawnOffset;

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
            
            Vector2 spawnViewport = GetRandomOutsidePoint();
            Vector3 spawnPos = _camera.ViewportToWorldPoint(new Vector3(spawnViewport.x, spawnViewport.y, _camera.nearClipPlane));
            spawnPos.z = 0;
            
            var enemy = _pool.GetObject();
            enemy.transform.position = spawnPos;
            enemy.gameObject.SetActive(true);

            if (enemy.TryGetComponent(out Enemy enemyComponent))
            {
                enemyComponent.Construct(_scoreData);
                
                // Vector3 targetPos = _camera.ViewportToWorldPoint(new Vector3(Random.Range(0.1f, 0.9f), Random.Range(0.1f, 0.9f), 0));
                // enemyComponent.SetDirection((targetPos - spawnPos).normalized);
            }
        }
        
        private Vector2 GetRandomOutsidePoint()
        {
            bool isVertical = Random.value > 0.5f;
            float edge = Random.value > 0.5f ? _spawnOffset : -(_spawnOffset - 1f); // Либо 1.1, либо -0.1
    
            return isVertical 
                ? new Vector2(Random.value, edge)   // Верх/Низ
                : new Vector2(edge, Random.value);  // Лево/Право
        }
    }
}
