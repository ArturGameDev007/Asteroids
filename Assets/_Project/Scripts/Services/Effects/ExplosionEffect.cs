using _Project.Scripts.Enemies;
using _Project.Scripts.Player.Weapons;
using Zenject;

namespace _Project.Scripts.Services.Effects
{
    public class ExplosionEffect : TimedPoolObject
    {
        private IObjectReturner<ExplosionEffect> _pool;
        
        [Inject]
        public void Construct(IObjectReturner<ExplosionEffect> pool)
        {
            _pool = pool;
        }
        
        protected override void ReturnToPool()
        {
            _pool?.ReturnPool(this);
        }
    }
}