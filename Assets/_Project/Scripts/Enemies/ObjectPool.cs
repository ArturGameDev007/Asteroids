using System.Collections.Generic;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class ObjectPool<T> : IObjectReturner<T> where T : Component
    {
        private readonly IInstantiator _instantiator;
        private readonly IRemoteConfigs _remoteConfigs;

        private readonly Queue<T> _pool;
        private readonly Transform _container;
        
        private readonly string _containerName;

        private int _initialCount;
        
        public T Prefab { get; set; }

        public ObjectPool(IInstantiator prefabInstantiator, IRemoteConfigs remoteConfigs, T prefab,
            string containerName, Transform parent)
        {
            _instantiator = prefabInstantiator;
            _remoteConfigs = remoteConfigs;

            Prefab = prefab;
            
            _containerName = containerName;

            _initialCount = GetSizePool();
            _pool = new Queue<T>(_initialCount);
            _container = new GameObject($"[Pool_{containerName}]").transform;
            _container.SetParent(parent);
        }

        public T GetObject()
        {
            if (Prefab == null)
                return null;

            if (!_pool.TryDequeue(out T objectType))
                objectType = CreateNewObject(true);

            objectType.gameObject.SetActive(true);

            return objectType;
        }

        public void ReturnPool(T objectType)
        {
            _pool.Enqueue(objectType);
            objectType.gameObject.SetActive(false);
        }

        private int GetSizePool()
        {
            var type = typeof(T);
            var minCountPoolSize = 0;

            if (type == typeof(Enemy))
            {
                if (_containerName.Contains("Asteroid"))
                    return _remoteConfigs.RemoteConfig.AsteroidPoolSize;
                
                if (_containerName.Contains("UFO"))
                    return _remoteConfigs.RemoteConfig.UfoPoolSize;
            }

            if (type == typeof(Bullet))
                return _remoteConfigs.RemoteConfig.BulletPoolSize;
            
            if (type == typeof(Laser))
                return _remoteConfigs.RemoteConfig.LaserPoolSize;

            return minCountPoolSize;
        }

        private T CreateNewObject(bool isActive)
        {
            var newObject = _instantiator.InstantiatePrefabForComponent<T>(Prefab, _container);

            newObject.gameObject.SetActive(isActive);

            if (!isActive)
                _pool.Enqueue(newObject);

            return newObject;
        }
    }
}