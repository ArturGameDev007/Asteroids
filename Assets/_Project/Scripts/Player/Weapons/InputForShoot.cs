using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class InputForShoot : MonoBehaviour
    {
        private readonly int _inputMouseLeft = 0;
        private readonly int _inputMouseRight = 1;

        public event Action<int> OnLaserChanged;
        public event Action<float> OnReplenishedLaserOverTime;

        [SerializeField] private GameObject _prefabLaser;
        [SerializeField] private GameObject _bulletPrefab;

        [SerializeField] private Transform _pointShootForlaser;
        [SerializeField] private Transform _pointShootForBullet;

        [SerializeField] private int MaxAmountLaser = 20;

        [SerializeField] private float _reloadTime = 5f;
        [SerializeField] private bool _isRealoding = false;

        private Coroutine _coroutine;

        public int CurrentAmmonLaser { get; private set; }

        private void Start()
        {
            CurrentAmmonLaser = MaxAmountLaser;
        }

        private void Update()
        {
            ShowInfo();

            if (Input.GetMouseButtonDown(_inputMouseLeft))
                ShootFromBullet();
            if (Input.GetMouseButtonDown(_inputMouseRight))
                Laser();
        }

        private IEnumerator ReloadLaser()
        {
            _isRealoding = true;

            float timer = 0f;
            float restoredTime = 5f;

            while (_reloadTime > timer)
            {
                _reloadTime -= Time.deltaTime;
                yield return null;
            }

            CurrentAmmonLaser = MaxAmountLaser;
            _reloadTime = restoredTime;
            _isRealoding = false;
        }

        private void Laser()
        {
            int minCountLazer = 0;

            if (CurrentAmmonLaser > minCountLazer)
            {
                CreateShoot(_prefabLaser, _pointShootForlaser.position, _pointShootForlaser.rotation);
                CurrentAmmonLaser--;

                if (CurrentAmmonLaser == minCountLazer)
                    _coroutine = StartCoroutine(ReloadLaser());
            }
        }

        private void ShootFromBullet()
        {
            if (_bulletPrefab != null)
                CreateShoot(_bulletPrefab, _pointShootForBullet.position, _pointShootForBullet.rotation);
        }

        private void CreateShoot(GameObject weapon, Vector3 position, Quaternion rotation)
        {
            Instantiate(weapon, position, rotation);
        }

        private void ShowInfo()
        {
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
            OnReplenishedLaserOverTime?.Invoke(_reloadTime);
        }
    }
}
