using _Project.Scripts.Enemies;
using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.Effects
{
    public class EffectResourceManager : IEffectResourceManager
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _explosionEffect;
        private readonly AssetReference _shootsEffect;

        private readonly ObjectPool<ExplosionEffect> _poolExplosion;
        private readonly ObjectPool<ShootEffect> _poolShoot;

        public EffectResourceManager(IResourceLoader resourceLoader, AssetReference explosionEffect,
            AssetReference shootsEffect, ObjectPool<ExplosionEffect> poolExplosion, ObjectPool<ShootEffect> poolShoot)
        {
            _resourceLoader = resourceLoader;
            _explosionEffect = explosionEffect;
            _shootsEffect = shootsEffect;
            _poolExplosion = poolExplosion;
            _poolShoot = poolShoot;
        }

        public async UniTask LoadEffects()
        {
            await UniTask.WhenAll(
                BindPrefabToPool(_explosionEffect, _poolExplosion),
                BindPrefabToPool(_shootsEffect, _poolShoot)
            );
        }

        private async UniTask BindPrefabToPool<T>(AssetReference assetReference, ObjectPool<T> pool) where T : Component
        {
            pool.Prefab = await _resourceLoader.LoadAssetAsync<T>(assetReference);
        }
    }
}