using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class DirectionShot : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private void Update()
        {
            DirectionMove();
        }

        private void DirectionMove()
        {
            transform.Translate(transform.up * _speed * Time.deltaTime, Space.World);
        }
    }
}
