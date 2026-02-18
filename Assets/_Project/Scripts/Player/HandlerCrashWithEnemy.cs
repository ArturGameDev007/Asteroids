using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class HandlerCrashWithEnemy : MonoBehaviour, ICollisionHandler
    {
        public event Action<IEnemy> OnCollisionDetected;

        private void OnValidate()
        {
            GetComponent<PolygonCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemy enemy))
                OnCollisionDetected?.Invoke(enemy);
        }
    }
}