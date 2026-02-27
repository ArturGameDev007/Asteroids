using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class Bullet : TimedPoolObject
    {
        private IObjectReturner<Bullet> _returner;

        public void Initialize(IObjectReturner<Bullet> returner)
        {
            _returner = returner;
        }

        protected override void ReturnToPool()
        {
            _returner?.ReturnPool(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy _))
                ReturnToPool();
        }
    }
}