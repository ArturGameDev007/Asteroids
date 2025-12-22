using Assets.Scripts.EnemySpace;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Bullet : TypesOfWeapon
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                DestroyOfEnemies(enemy);
                Destroy(gameObject);
                //ScoreManager.Instance.AddScore(PointForKill);
            }
        }

        protected override void DestroyOfEnemies(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
