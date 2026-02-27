using _Project.Scripts.Enemies;

namespace _Project.Scripts.Player.Weapons
{
    public class Laser : TimedPoolObject
    {
        private IObjectReturner<Laser> _returner;

        public void Initialize(IObjectReturner<Laser> returner)
        {
            _returner = returner;
        }
        
        protected override void ReturnToPool()
        {
            _returner?.ReturnPool(this);
        }
    }
}
