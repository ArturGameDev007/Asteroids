using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public class ShotLaser : MonoBehaviour
    {
        [SerializeField] private Laser _prefab;
        [SerializeField] private Transform _point;
        [SerializeField] private int _maxAmountShots = 5;

        private int _currentCountLaser;

        private void Start()
        {
            _currentCountLaser = _maxAmountShots;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shoot();
        }

        private void Shoot()
        {
            int minCountlazer = 0;

            if (_currentCountLaser > minCountlazer)
            {
                Instantiate(_prefab, _point.position, _point.rotation);
                _currentCountLaser--;
            }
            else
            {
                Debug.Log("Нет зарядов лазера.");
            }
        }
    }
}
