using Assets.Scripts.EnemySpace;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Laser : TypesOfWeapon
    {
        //ScoreManager manager = new ScoreManager();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                DestroyOfEnemies(enemy);
                ScoreManager.Instance.AddScore(PointForKill);
            }
        }

        protected override void DestroyOfEnemies(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
