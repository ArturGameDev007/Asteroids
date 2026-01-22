using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private ObjectPool _pool;

        private ScoreData _scoreData;
        
        public void Initialize(ObjectPool pool, ScoreData  scoreData)
        {
            _pool = pool;
            _scoreData = scoreData;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                _pool.PutObject(this);
                _scoreData.AddScore();
                Destroy(bullet.gameObject);
            }
            else if (other.TryGetComponent(out Laser laser))
            {
                _scoreData.AddScore();
                _pool.PutObject(this);
            }
        }
    }
}
