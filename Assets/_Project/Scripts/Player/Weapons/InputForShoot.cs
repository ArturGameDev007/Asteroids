using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class InputForShoot : MonoBehaviour, IShootable
    {
        private const int INPUT_MOUSE_LEFT = 0;
        private const int INPUT_MOUSE_RIGHT = 1;

        [Header("Prefabs Weapons")]
        [SerializeField] private GameObject _prefabLaser;
        [SerializeField] private GameObject _bulletPrefab;

        [Header("Points Weapons")]
        [SerializeField] private Transform _pointShootForlaser;
        [SerializeField] private Transform _pointShootForBullet;

        private GenerateLaser _laserAmmo;
        private WeaponShooter _shooter;

        public void Initialize(GenerateLaser laser, WeaponShooter shooter)
        {
            _laserAmmo = laser;
            _shooter = shooter;
        }

        private void Update()
        {
            InputBulletShoot();
            InputLaserShoot();
        }

        public void EnableControl()
        {
            enabled = true;
        }

        public void DisableControl()
        {
            enabled = false;
        }

        private void InputBulletShoot()
        {
            if (Input.GetMouseButtonDown(INPUT_MOUSE_LEFT))
                _shooter.ShootBullet(_pointShootForBullet);
        }

        private void InputLaserShoot()
        {
            if (Input.GetMouseButtonDown(INPUT_MOUSE_RIGHT))
                if (_laserAmmo.TrySpendAmmo())
                    _shooter.ShootLaser(_pointShootForlaser);
        }
    }
}