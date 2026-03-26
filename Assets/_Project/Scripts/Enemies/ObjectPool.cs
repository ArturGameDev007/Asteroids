using System.Collections.Generic;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class ObjectPool<T> : IObjectReturner<T> where T : Component
    {
        private readonly IInstantiator _instantiator;

        private readonly Queue<T> _pool;
        private readonly Transform _container;

        public T Prefab { get; set; }
        private Character _player;

        public ObjectPool(IInstantiator prefabInstantiator, T prefab, int initialCount,
            string containerName, Transform parent)
        {
            _instantiator = prefabInstantiator;
            Prefab = prefab;
            _pool = new Queue<T>(initialCount);
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