using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Scripts.Player.Weapons
{
    public class GenerateLaser : MonoBehaviour
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        [SerializeField] private int _maxAmountLaser = 20;
        [SerializeField] private float _reloadTime = 5f;

        public int CurrentAmmonLaser { get; private set; }
        public bool IsRealoding { get; private set; }

        private Coroutine _coroutine;

        private void Start()
        {
            CurrentAmmonLaser = _maxAmountLaser;
        }

        private void Update()
        {
            ShoInfo();
        }

        public bool TrySpendAmmo()
        {
            int minCountLazer = 0;

            if (IsRealoding || CurrentAmmonLaser <= 0)
                return false;

            if (CurrentAmmonLaser > minCountLazer)
            {
                CurrentAmmonLaser--;

                if (CurrentAmmonLaser == minCountLazer)
                    _coroutine = StartCoroutine(ReloadLaser());
            }


            return true;
            //if (IsRealoding || CurrentAmmonLaser <= 0)
            //    return false;

            //CurrentAmmonLaser--;
            //OnLaserChanged?.Invoke(CurrentAmmonLaser);

            //if (CurrentAmmonLaser <= 0)
            //    _coroutine = StartCoroutine(ReloadLaser());

        }

        private IEnumerator ReloadLaser()
        {
            //IsRealoding = true;
            //float timer = _reloadTime;

            //while (timer > 0)
            //{
            //    timer -= Time.deltaTime;
            //    OnReloadProgress?.Invoke(timer);
            //    yield return null;
            //}

            //CurrentAmmonLaser = _maxAmountLaser;
            //OnLaserChanged?.Invoke(CurrentAmmonLaser);
            //IsRealoding = false;

            IsRealoding = true;

            float timer = 0f;
            float restoredTime = 5f;

            while (_reloadTime > timer)
            {
                _reloadTime -= Time.deltaTime;
                //OnReloadProgress?.Invoke(timer);
                yield return null;
            }

            CurrentAmmonLaser = _maxAmountLaser;
            _reloadTime = restoredTime;
            IsRealoding = false;
        }

        public void ShoInfo()
        {
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
            OnReloadProgress?.Invoke(_reloadTime);
        }
    }
}
