using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Services.Effects
{
    public class EffectSystem : IEffectService
    {
        private readonly ObjectPool<ExplosionEffect> _poolExplosion;
        private readonly ObjectPool<ShootEffect> _poolShoot;
        
        private readonly IEffectResourceManager _resourceManager;

        public EffectSystem(ObjectPool<ExplosionEffect> poolExplosion, ObjectPool<ShootEffect> poolShoot,
            IEffectResourceManager resourceManager)
        {
            _poolExplosion = poolExplosion;
            _poolShoot = poolShoot;
            _resourceManager = resourceManager;
        }

        public void PlayExplosionForKill(Vector3 position)
        {
            if (_poolExplosion.Prefab == null)
            {
                _poolExplosion.Prefab = _resourceManager.ExplosionPrefab;
            }
            
            PlayEffect(_poolExplosion, position, Quaternion.identity);
        }

        public void PlayShoot(Vector3 position, Quaternion rotation)
        {
            if (_poolShoot.Prefab == null)
            {
                _poolShoot.Prefab = _resourceManager.ShootsPrefab;
            }

            PlayEffect(_poolShoot, position, rotation);
        }

        private T PlayEffect<T>(ObjectPool<T> effectPool, Vector3 position, Quaternion rotation) where T : MonoBehaviour
        {
            if (effectPool == null)
                return null;

            var effectInstance = effectPool.GetObject();

            if (effectInstance == null)
                return null;

            effectInstance.transform.SetPositionAndRotation(position, rotation);

            if (effectInstance.TryGetComponent(out ParticleSystem effect))
                effect.Play();

            return effectInstance;
        }
    }
}