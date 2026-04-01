using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public class InputForShoot : MonoBehaviour, IInputPauseHandler
    {
        private const int INPUT_MOUSE_LEFT = 0;
        private const int INPUT_MOUSE_RIGHT = 1;

        [Header("Points Weapons")]
        [SerializeField] private Transform _pointShootForlaser;
        [SerializeField] private Transform _pointShootForBullet;

        private IPlayerProvider _playerProvider;
        private IWeaponShooter _shooter;

        private bool _isPaused = true;

        [Inject]
        public void Construct(IPlayerProvider laser, IWeaponShooter shooter)
        {
            _playerProvider = laser;
            _shooter = shooter;
            
            _isPaused = false;
        }

        private void Update()
        {
            if (_isPaused || _playerProvider == null || _shooter == null)
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
                if (_playerProvider.LaserState.TrySpendAmmo())
                    _shooter.ShootLaser(_pointShootForlaser);
        }
    }
}