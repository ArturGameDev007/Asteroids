using Assets.Scripts.Player.Weapons;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                Destroy(gameObject);
                Destroy(bullet.gameObject);

                AddScoreForDestroed();
            }
            else if (other.TryGetComponent(out Laser laser))
            {
                AddScoreForDestroed();
                Destroy(gameObject);
            }
        }

        private void AddScoreForDestroed()
        {
            ScoreManager.Instance.AddScore();
        }
    }
}
