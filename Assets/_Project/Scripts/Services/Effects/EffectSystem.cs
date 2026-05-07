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
            var effect = _poolExplosion.GetObject();
            effect.transform.position = position;
            effect.Play();
            
            ReturnPool(effect, _poolExplosion).Forget();
        }

        public void PlayShoot(Vector3 position, Quaternion rotation)
        {
            var effect = _poolShoot.GetObject();
            effect.transform.position = position;
            effect.transform.rotation = rotation;
            effect.Play();
            
            ReturnPool(effect, _poolShoot).Forget();
        }

        private async UniTaskVoid ReturnPool(ParticleSystem effect, ObjectPool<ParticleSystem> pool)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(effect.main.duration));
        
            if (effect != null)
                pool.ReturnPool(effect);
        }
    }
}