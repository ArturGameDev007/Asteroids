using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private ScoreData _scoreData;
        private Vector2 _direction;

        public void Construct(ScoreData scoreData)
        {
            _scoreData = scoreData;
        }
        
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                _scoreData.AddScore();

                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
            else if (other.TryGetComponent(out Laser laser))
            {
                _scoreData.AddScore();

                Destroy(gameObject);
            }
        }
    }
}
