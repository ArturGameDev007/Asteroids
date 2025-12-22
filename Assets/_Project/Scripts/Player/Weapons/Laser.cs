using Assets.Scripts.EnemySpace;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class Laser : TypesOfWeapon
    {
        public event Action OnHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                DestroyOfEnemies(enemy);
                //ScoreManager.Instance.AddScore(PointForKill);
                OnHit?.Invoke();
            }
        }

        protected override void DestroyOfEnemies(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}
