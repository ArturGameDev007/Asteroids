using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shot();
            }
        }

        private void Shot()
        {
            Instantiate(_prefab, transform.position, _prefab.transform.rotation);
        }
    }
}
