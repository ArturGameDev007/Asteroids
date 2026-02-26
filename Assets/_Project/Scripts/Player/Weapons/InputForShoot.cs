using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class InputForShoot : MonoBehaviour
    {
        private const int INPUT_MOUSE_LEFT = 0;
        private const int INPUT_MOUSE_RIGHT = 1;

        [Header("Prefabs Weapons")]
        [SerializeField] private Laser _prefabLaser;
        [SerializeField] private Bullet _bulletPrefab;

        [Header("Points Weapons")]
        [SerializeField] private Transform _pointShootForlaser;
        [SerializeField] private Transform _pointShootForBullet;

        private GenerateLaser _laserAmmo;
        private WeaponShooter _shooter;
        
        private bool _isPaused;

        public void Initialize(GenerateLaser laser, WeaponShooter shooter)
        {
            _laserAmmo = laser;
            _shooter = shooter;
        }

        private void Update()
        {
            if (_isPaused)
                return;
            
            InputBulletShoot();
            InputLaserShoot();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public void StopShoots()
        {
            _shooter?.StopAllShoots();
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