using Assets.Scripts.EnemySpace;
using System;
using UnityEngine;

public class HandlerCrashWithEnemy : MonoBehaviour
{
    public event Action<IEnemy> CollisionHandler;

    private void OnValidate()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IEnemy enemy))
        {
            CollisionHandler?.Invoke(enemy);
        }
    }
}
