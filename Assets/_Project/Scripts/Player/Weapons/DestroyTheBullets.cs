using System;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class DestroyTheBullets : MonoBehaviour
    {
        private const string ENEMY = "Enemy";
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ENEMY))
            {
                Destroy(gameObject);
            }
        }
    }
}