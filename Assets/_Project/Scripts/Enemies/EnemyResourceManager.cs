using System;
using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class EnemyResourceManager
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference[] _enemyReferences;
        private readonly ObjectPool<Enemy>[] _pools;

        public EnemyResourceManager(IResourceLoader resourceLoader, AssetReference[] assetReferences,
            [Inject(Id = "AsteroidPool")] ObjectPool<Enemy> asteroidPool,
            [Inject(Id = "UfoPool")] ObjectPool<Enemy> ufoPool)
        {
            _resourceLoader = resourceLoader;
            _enemyReferences = assetReferences;
            _pools = new[] { asteroidPool, ufoPool };
        }

        public async UniTask LoadEnemiesAsync()
        {
            var prefabs = new UniTask<Enemy>[_enemyReferences.Length];

            for (int i = 0; i < _enemyReferences.Length; i++)
                prefabs[i] = _resourceLoader.LoadAssetAsync<Enemy>(_enemyReferences[i]);

            Enemy[] loadedEnemy = await UniTask.WhenAll(prefabs);

            int minCount = Math.Min(loadedEnemy.Length, _pools.Length);

            for (int i = 0; i < minCount; i++)
                _pools[i].Prefab = loadedEnemy[i];
        }

        public void UnloadEnemies()
        {
            foreach (var enemy in _enemyReferences)
                _resourceLoader.UnloadAsset(enemy);
        }
    }
}