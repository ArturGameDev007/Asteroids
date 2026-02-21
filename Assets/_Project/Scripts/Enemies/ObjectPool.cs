using System.Collections.Generic;
using _Project.Scripts.Player;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Enemies
{
    public class ObjectPool<T> : IObjectReturner<T> where T : Component
    {
        private List<T> _prefabs;
        private Queue<T> _pool;
        private Transform _container;

        private Character _player;

        public ObjectPool(List<T> prefabs, int initialCount, string containerName, Transform parent)
        {
            _prefabs = prefabs;
            _pool = new Queue<T>(initialCount);
            _container = new GameObject($"[Pool_{containerName}]").transform;
            _container.SetParent(parent);

            AddObjects(initialCount);
        }

        public T GetObject()
        {
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

        public void ClearPool()
        {
            _pool.Clear();
        }

        private void AddObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                CreateNewObject(false);
            }
        }

        private T CreateNewObject(bool isActive)
        {
            int minCountPool = 0;

            var prefab = _prefabs[Random.Range(minCountPool, _prefabs.Count)];
            var newObject = Object.Instantiate(prefab, _container);

            newObject.gameObject.SetActive(isActive);

            if (!isActive)
                _pool.Enqueue(newObject);

            return newObject;
        }
    }
}