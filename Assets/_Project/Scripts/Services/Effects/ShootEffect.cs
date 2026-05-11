using _Project.Scripts.Enemies;
using _Project.Scripts.Player.Weapons;
using Zenject;

namespace _Project.Scripts.Services.Effects
{
    public class ShootEffect : TimedPoolObject
    {
        private IObjectReturner<ShootEffect> _pool;

        [Inject]
        public void Construct(IObjectReturner<ShootEffect> pool)
        {
            _pool = pool;
        }

        protected override void ReturnToPool()
        {
            _pool?.ReturnPool(this);
        }
    }
}