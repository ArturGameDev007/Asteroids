using Assets._Project.Scripts.Player.Weapons;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class InputForShoot : MonoBehaviour
    {
        private const int InputMouseLeft = 0;
        private const int InputMouseRight = 1;

        [SerializeField] private GameObject _prefabLaser;
        [SerializeField] private GameObject _bulletPrefab;

        [Space(10)]
        [SerializeField] private Transform _pointShootForlaser;
        [SerializeField] private Transform _pointShootForBullet;

        [Space(10)]
        [SerializeField] private GenerateLaser _laserAmmo;
        [SerializeField] private WeaponShooter _shooter;

        private void Update()
        {
            InputBulletShoot();
            InputlaserShoot();
        }

        private void InputBulletShoot()
        {
            if (Input.GetMouseButtonDown(InputMouseLeft))
                _shooter.CreateShoot(_bulletPrefab, _pointShootForBullet);
        }

        private void InputlaserShoot()
        {
            if (Input.GetMouseButtonDown(InputMouseRight))
                if (_laserAmmo.TrySpendAmmo())
                    _shooter.CreateShoot(_prefabLaser, _pointShootForlaser);
        }
    }
}
