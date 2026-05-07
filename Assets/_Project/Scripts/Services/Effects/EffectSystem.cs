using System;
using _Project.Scripts.Enemies;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Services.Effects
{
    public class EffectSystem : IEffectService
    {
        private readonly ObjectPool<ParticleSystem> _poolExplosion;
        private readonly ObjectPool<ParticleSystem> _poolShoot;

        public EffectSystem([Inject(Id = "ExplosionPool")] ObjectPool<ParticleSystem> poolExplosion, [Inject(Id = "ShootsPool")] ObjectPool<ParticleSystem> poolShoot)
        {
            _poolExplosion = poolExplosion;
            _poolShoot = poolShoot;
        }

        public void PlayExplosionForKill(Vector3 position)
        {
            var effect = PlayEffect(_poolExplosion, position,  Quaternion.identity);
            
            ReturnPool(effect, _poolExplosion).Forget();
        }

        public void PlayShoot(Vector3 position, Quaternion rotation)
        {

            var effect = PlayEffect(_poolShoot, position,  rotation);
            
            ReturnPool(effect, _poolShoot).Forget();
        }

        private ParticleSystem PlayEffect(ObjectPool<ParticleSystem> effectPool, Vector3 position, Quaternion rotation)
        {
            var  effectInstance = effectPool.GetObject();
            effectInstance.transform.SetPositionAndRotation(position, rotation);
            effectInstance.Play();
            
            return effectInstance;
        }

        private async UniTaskVoid ReturnPool(ParticleSystem effect, ObjectPool<ParticleSystem> pool)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(effect.main.duration));
        
            if (effect != null)
                pool.ReturnPool(effect);
        }
    }
}