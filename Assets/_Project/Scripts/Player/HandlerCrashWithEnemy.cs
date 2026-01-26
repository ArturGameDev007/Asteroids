using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class HandlerCrashWithEnemy : MonoBehaviour
    {
        public event Action<Enemy> OnCollisionHandler;

        private void OnValidate()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                OnCollisionHandler?.Invoke(enemy);
        }
    }
}