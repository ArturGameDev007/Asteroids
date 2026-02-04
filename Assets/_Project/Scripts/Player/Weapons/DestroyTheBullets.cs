using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class DestroyTheBullets : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy _))
            {
                Destroy(gameObject);
            }
        }
    }
}