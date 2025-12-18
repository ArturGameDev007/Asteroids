using Assets.Scripts.EnemySpace;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Bullet : TypesOfWeapon
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                ScoreManager.Instance.AddScore(PointForKill);
                DestroyOfEnemies(enemy);
                Destroy(gameObject);
            }
        }

        protected override void DestroyOfEnemies(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
