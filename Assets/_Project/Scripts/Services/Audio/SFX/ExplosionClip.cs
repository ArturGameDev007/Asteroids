using _Project.Scripts.Enemies;
using _Project.Scripts.Player.Weapons;
using Zenject;

namespace _Project.Scripts.Services.Audio.SFX
{
    public class ExplosionClip: TimedPoolObject
    {
        private IObjectReturner<ExplosionClip> _pool;
        
        [Inject]
        public void Construct(IObjectReturner<ExplosionClip> pool)
        {
            _pool = pool;
        }

        protected override void ReturnToPool()
        {
            _pool?.ReturnPool(this);
        }
    }
}