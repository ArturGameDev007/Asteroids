using Assets.Scripts.EnemySpace;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Laser : TypesOfWeapon
    {
        //private int _pointForKill = 10;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                DestroyOfEnemies(enemy);
            }

            if (other.TryGetComponent(out ScoreManager player))
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
