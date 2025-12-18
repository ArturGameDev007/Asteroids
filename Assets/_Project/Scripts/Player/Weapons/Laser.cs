using Assets.Scripts.EnemySpace;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Laser : TypesOfWeapon
    {
        private ScoreManager _manager;

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (other.TryGetComponent(out Enemy enemy))
            {
                _manager.AddScore(PointForKill);
                //ScoreManager.Instance.AddScore(PointForKill);
                DestroyOfEnemies(enemy);
            }
        }

        protected override void DestroyOfEnemies(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
