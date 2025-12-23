using Assets._Project.Scripts.UI.GameScreen;
using Assets.Scripts.Player.Weapons;
using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private ScoreData _scoreData;

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
