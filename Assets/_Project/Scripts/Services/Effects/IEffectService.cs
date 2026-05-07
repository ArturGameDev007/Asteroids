using UnityEngine;

namespace _Project.Scripts.Services.Effects
{
    public interface IEffectService
    {
        public void PlayExplosionForKill(Vector3 position);
        
        public void PlayShoot(Vector3 position, Quaternion rotation);
    }
}