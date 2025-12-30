using Scripts.EnemySpace;
using System;
using UnityEngine;

namespace Scripts.Player
{
    public class HandlerCrashWithEnemy : MonoBehaviour
    {
        public event Action<IEnemy> OnCollisionHandler;

        private void OnValidate()
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out IEnemy enemy))
                OnCollisionHandler?.Invoke(enemy);
        }
    }
}

