using System.Collections;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [Header("ObjectPool Enemies")]
        [SerializeField] private ObjectPool _pool;

        [Space(10)]
        [Header("Spawn Positions")]
        [SerializeField] private float _positionX;
        [SerializeField] private float _positionY;

        private float _delay = 3f;

        private ScoreData _scoreData;

        private Coroutine _coroutine;

        public void Initialize(ScoreData scoreData)
        {
            _scoreData = scoreData;
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
            float positionX = Random.Range(-_positionX, _positionX);
            Vector2 positionSpawn = new Vector2(positionX, _positionY);

            var enemy = _pool.GetObject();

            if (enemy.TryGetComponent(out Enemy enemyComponent))
            {
                enemyComponent.Construct(_scoreData);
            }

            enemy.gameObject.SetActive(true);
            enemy.transform.position = positionSpawn;
        }
    }
}
