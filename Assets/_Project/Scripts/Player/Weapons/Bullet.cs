using Assets.Scripts.EnemySpace;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Bullet : TypesOfWeapon
    {
        //private int _pointForKill = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                DestroyOfEnemies(enemy);
                Destroy(gameObject);
            }

            if (other.TryGetComponent(out Player player))
            {
                player.AddScore(PointForKill);
            }
        }

        protected override void DestroyOfEnemies(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
