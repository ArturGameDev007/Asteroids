using System;
using System.Collections.Generic;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Player;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Enemies
{
    public class ObjectPool<T> where T : Component
    {
        // private List<Enemy> _prefabEnemy;
        // private Queue<Enemy> _pool;

        private List<T> _prefabs;
        private Queue<T> _pool;
        private Transform _container;
        
        private Action<T> _onInit;

        private Character _player;

        public ObjectPool(List<T> prefabs, int initialCount,  string containerName, Transform parent,  Action<T> onInit = null)
        {
            _prefabs = prefabs;
            _pool = new Queue<T>(initialCount);
            _container = new GameObject($"[Pool_{containerName}]").transform;
            _container.SetParent(parent);
            
            _onInit = onInit;

            for (int i = 0; i < initialCount; i++)
                CreateNewObject(false);
        }

        // public ObjectPool(List<Enemy> prefabEnemy)
        // {
        //     _prefabEnemy = prefabEnemy;
        //     _pool = new Queue<Enemy>();
        // }

        // public void Initialize(Character player)
        // {
        //     _player = player;
        //     // _container = new GameObject("Container_For_Enemies");
        // }

        public T GetObject()
        {
            if (!_pool.TryDequeue(out T objectType))
                objectType = CreateNewObject(true);

            objectType.gameObject.SetActive(true);
            
            return objectType;
        }

        // private T CreateNewObject()
        // {
        //     int minCountPool = 0;
        //     
        //     int indexEnemy = Random.Range(minCountPool, _prefab.Count);
        //     var objectType = Object.Instantiate(_prefab[indexEnemy], _container.transform);
        //     
        //     if (objectType.TryGetComponent(out FlyingSaucerController saucer))
        //         saucer.Construct(_player.transform);
        //     
        //     return objectType;
        //
        // }
        
        
        private T CreateNewObject(bool isActive)
        {
            T prefab = _prefabs[Random.Range(0, _prefabs.Count)];
            T obj = Object.Instantiate(prefab, _container);
            
            obj.gameObject.SetActive(isActive);
            
            if (!isActive) 
                _pool.Enqueue(obj);
            
            return obj;
        }

        public void PutObject(T objectType)
        {
            _pool.Enqueue(objectType);
            objectType.gameObject.SetActive(false);
        }

        public void ClearPool()
        {
            _pool.Clear();
        }
    }
}