using Scripts.Player.Weapons;
using Scripts.UI.GameScreen;
using UnityEngine;

namespace Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private ScoreData _scoreData;

        public void Construct(ScoreData scoreData)
        {
            _scoreData = scoreData;
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
