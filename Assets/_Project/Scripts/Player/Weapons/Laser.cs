using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public class Laser : TimedPoolObject
    {
        private IObjectReturner<Laser> _returner;

        [Inject]
        public void Construct(IObjectReturner<Laser> returner)
        {
            _returner = returner;
        }
        
        protected override void ReturnToPool()
        {
            _returner?.ReturnPool(this);
        }
    }
}
