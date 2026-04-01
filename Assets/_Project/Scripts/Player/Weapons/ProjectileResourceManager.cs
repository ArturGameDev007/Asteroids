using _Project.Scripts.Enemies;
using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Player.Weapons
{
    public class ProjectileResourceManager
    {
        private readonly IResourceLoader _resourceLoader;

        private readonly AssetReference _bulletReference;
        private readonly AssetReference _laserReference;

        private readonly ObjectPool<Bullet> _bulletPool;
        private readonly ObjectPool<Laser> _laserPool;
        
        public ProjectileResourceManager(IResourceLoader resourceLoader, AssetReference bulletReference,
            AssetReference laserReference, ObjectPool<Bullet> bulletPool, ObjectPool<Laser> laserPool)
        {
            _resourceLoader = resourceLoader;
            _bulletReference = bulletReference;
            _laserReference = laserReference;
            _bulletPool = bulletPool;
            _laserPool = laserPool;
        }

        public async UniTask LoadShotsAsync()
        {
            await UniTask.WhenAll(
                BindPrefabToPool(_bulletReference, _bulletPool),
                BindPrefabToPool(_laserReference, _laserPool)
            );
        }

        public void UnloadShots()
        {
            _resourceLoader.UnloadAsset(_bulletReference);
            _resourceLoader.UnloadAsset(_laserReference);
        }

        private async UniTask BindPrefabToPool<T>(AssetReference assetReference, ObjectPool<T> pool) where T : Component
        {
            pool.Prefab = await _resourceLoader.LoadAssetAsync<T>(assetReference);
        }
    }
}