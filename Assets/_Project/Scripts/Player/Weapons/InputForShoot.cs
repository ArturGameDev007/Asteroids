using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class InputForShoot : MonoBehaviour
    {
        private const int INPUT_MOUSE_LEFT = 0;
        private const int INPUT_MOUSE_RIGHT = 1;

        [SerializeField] private GameObject _prefabLaser;
        [SerializeField] private GameObject _bulletPrefab;

        [Space(10)]
        [SerializeField] private Transform _pointShootForlaser;
        [SerializeField] private Transform _pointShootForBullet;

        [Space(10)]
        [SerializeField] private GenerateLaser _laserAmmo;
        [SerializeField] private WeaponShooter _shooter;

        public void Initialize(GenerateLaser laser)
        {
            _laserAmmo = laser;
        }
        
        public void Construct(GenerateLaser laser)
        {
            _laserAmmo = laser;
        }
        
        private void Update()
        {
            InputBulletShoot();
            InputLaserShoot();
        }

        private void InputBulletShoot()
        {
            if (Input.GetMouseButtonDown(INPUT_MOUSE_LEFT))
                _shooter.CreateShoot(_bulletPrefab, _pointShootForBullet);
        }

        private void InputLaserShoot()
        {
            if (Input.GetMouseButtonDown(INPUT_MOUSE_RIGHT))
                if (_laserAmmo.TrySpendAmmo())
                    _shooter.CreateShoot(_prefabLaser, _pointShootForlaser);
        }
    }
}
