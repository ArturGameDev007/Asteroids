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

        //[SerializeField] private bool _isRealoding = false;

        public int CurrentAmmonLaser { get; private set; }
        public bool IsRealoding { get; private set; }

        private Coroutine _coroutine;

        private void Start()
        {
            CurrentAmmonLaser = _maxAmountLaser;
        }

        public bool TrySpendAmmo()
        {
            if (IsRealoding || CurrentAmmonLaser <= 0)
                return false;

            CurrentAmmonLaser--;
            OnLaserChanged?.Invoke(CurrentAmmonLaser);

            if (CurrentAmmonLaser <= 0)
                _coroutine = StartCoroutine(ReloadLaser());

            return true;
        }

        private IEnumerator ReloadLaser()
        {
            IsRealoding = true;
            float timer = _reloadTime;

            while (timer > 0)
            {
                timer -= Time.deltaTime;
                OnReloadProgress?.Invoke(timer);
                yield return null;
            }

            CurrentAmmonLaser = _maxAmountLaser;
            OnLaserChanged?.Invoke(CurrentAmmonLaser);
            IsRealoding = false;

            //_isRealoding = true;

            //float timer = 0f;
            //float restoredTime = 5f;



            //while (_reloadTime > timer)
            //{
            //    _reloadTime -= Time.deltaTime;
            //    OnReloadProgress?.Invoke(timer);
            //    yield return null;
            //}

            //CurrentAmmonLaser = _maxAmountLaser;
            //_reloadTime = restoredTime;
            //_isRealoding = false;
        }
    }
}
