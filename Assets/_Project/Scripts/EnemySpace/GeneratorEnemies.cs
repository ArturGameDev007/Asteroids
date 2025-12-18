using System.Collections;
using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class GeneratorEnemies : MonoBehaviour
    {
        [SerializeField] private float _positionX;
        [SerializeField] private float _positionY;

        [SerializeField] private ObjectPool _pool;

        private float _delay = 3f;

        private Coroutine _coroutine;

        private void Start()
        {
            _coroutine = StartCoroutine(GeneratorEnemy(_delay));
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
            enemy.gameObject.SetActive(true);

            enemy.transform.position = positionSpawn;
        }
    }
}
