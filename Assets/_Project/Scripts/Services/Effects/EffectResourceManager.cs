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

        public ExplosionEffect ExplosionPrefab { get; private set; }
        public ShootEffect ShootsPrefab { get; private set; }

        public EffectResourceManager(IResourceLoader resourceLoader, AssetReference explosionEffect,
            AssetReference shootsEffect)
        {
            _resourceLoader = resourceLoader;
            _explosionEffect = explosionEffect;
            _shootsEffect = shootsEffect;
        }

        public async UniTask LoadEffects()
        {
            ExplosionPrefab = await _resourceLoader.LoadAssetAsync<ExplosionEffect>(_explosionEffect);
            ShootsPrefab = await _resourceLoader.LoadAssetAsync<ShootEffect>(_shootsEffect);
        }
    }
}