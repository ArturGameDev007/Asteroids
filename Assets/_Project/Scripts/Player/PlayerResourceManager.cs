using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Player
{
    public class PlayerResourceManager
    {
        private readonly IInstantiator _instantiator;
        private readonly IPlayerProvider _playerProvider;

        private readonly PlayerController _player;
        private readonly IResourceLoader _resourceLoader;
        private readonly AssetReference _playerReference;

        public PlayerResourceManager(IInstantiator instantiator, IResourceLoader resourceLoader,
            AssetReference playerReference, IPlayerProvider playerProvider)
        {
            _instantiator = instantiator;
            _resourceLoader = resourceLoader;
            _playerReference = playerReference;
            _playerProvider = playerProvider;
        }

        // public PlayerResourceManager(IInstantiator instantiator, IPlayerProvider playerProvider,
        //     PlayerController player)
        // {
        //     _instantiator = instantiator;
        //     _playerProvider = playerProvider;
        //     _player = player;
        // }

        public async UniTask<PlayerController> LoadAsync()
        {
            var prefab = await _resourceLoader.LoadAssetAsync<PlayerController>(_playerReference);
            var asset = _instantiator.InstantiatePrefabForComponent<PlayerController>(prefab);
     
            _playerProvider.Setup(asset);
        
            return asset;
        }


        // public PlayerController GetPrefab()
        // {
        //     var prefab = _instantiator.InstantiatePrefab(_player.gameObject);
        //     var controller = prefab.GetComponent<PlayerController>();
        //
        //     _playerProvider.Setup(controller);
        //
        //     return controller;
        // }

        public void Unload()
        {
            _resourceLoader.UnloadAsset(_playerReference);
        }
    }
}