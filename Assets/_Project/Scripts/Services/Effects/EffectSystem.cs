using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Services.Effects
{
    public class EffectSystem : IEffectService
    {
        private readonly ObjectPool<ExplosionEffect> _poolExplosion;
        private readonly ObjectPool<ShootEffect> _poolShoot;
        
        public EffectSystem(ObjectPool<ExplosionEffect> poolExplosion, ObjectPool<ShootEffect> poolShoot)
        {
            _poolExplosion = poolExplosion;
            _poolShoot = poolShoot;
        }

        public void PlayExplosionForKill(Vector3 position)
        {
            PlayEffect(_poolExplosion, position, Quaternion.identity);
        }

        public void PlayShoot(Vector3 position, Quaternion rotation)
        {
            PlayEffect(_poolShoot, position, rotation);
        }

        private void PlayEffect<T>(ObjectPool<T> effectPool, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            if (effectPool == null)
                return;

            var effectInstance = effectPool.GetObject();
            effectInstance.transform.SetPositionAndRotation(position, rotation);

            if (effectInstance.TryGetComponent(out ParticleSystem effect))
                effect.Play();
        }
    }
}