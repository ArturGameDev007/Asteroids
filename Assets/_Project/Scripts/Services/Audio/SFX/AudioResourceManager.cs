using _Project.Scripts.Enemies;
using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Services.Audio.SFX
{
    public class AudioResourceManager : IAudioResourceManager
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _explosionClip;
        private readonly AssetReference _shootsClip;

        private readonly ObjectPool<ExplosionClip> _poolExplosionClip;
        private readonly ObjectPool<ShootClip> _poolShootClip;

        public AudioResourceManager(IResourceLoader resourceLoader, AssetReference explosionClip,
            AssetReference shootsClip, ObjectPool<ExplosionClip> poolExplosionClip, ObjectPool<ShootClip> poolShootClip)
        {
            _resourceLoader = resourceLoader;
            _explosionClip = explosionClip;
            _shootsClip = shootsClip;
            _poolExplosionClip = poolExplosionClip;
            _poolShootClip = poolShootClip;
        }

        public async UniTask LoadClips()
        {
            await UniTask.WhenAll(
                BindPrefabToPool(_explosionClip, _poolExplosionClip),
                BindPrefabToPool(_shootsClip, _poolShootClip)
            );
        }

        private async UniTask BindPrefabToPool<T>(AssetReference assetReference, ObjectPool<T> pool) where T : Component
        {
            pool.Prefab = await _resourceLoader.LoadAssetAsync<T>(assetReference);
        }
    }
}