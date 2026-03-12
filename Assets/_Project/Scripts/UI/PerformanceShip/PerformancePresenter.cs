using System;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformancePresenter: IInitializable, ITickable, IDisposable
    {
        private readonly ICoordinateView _coordinateView;
        private readonly ILaserView _laserView;
        private readonly PlayerController _shipController;
        private readonly GenerateLaser _laser;
        private readonly Rigidbody2D _rigidbody2D;

        public PerformancePresenter(ICoordinateView coordinateView, ILaserView laserView, PlayerController shipController, GenerateLaser laser)
        {
            _coordinateView = coordinateView;
            _laserView = laserView;
            _shipController = shipController;
            _laser = laser;
            _rigidbody2D = shipController.GetComponent<Rigidbody2D>();
        }

        public void Initialize()
        {
            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReloadProgress += OnShowRollbackLaser;

            OnShowInfoLaser(_laser.CurrentAmmonLaser);
            OnShowRollbackLaser(_laser.LaserConfig.ReloadTime);
        }

        public void Tick()
        {
            Vector3 direction = _shipController.transform.position;

            float rotationAngleZ = _shipController.transform.localEulerAngles.z;
            float speed = _rigidbody2D.velocity.magnitude;

            string displayText = string.Format("<b>" + "X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}" + "</b>",
                direction.x, direction.y, rotationAngleZ, speed);

            _coordinateView.SetCoordinateText(displayText);
        }

        public void Dispose()
        {
            _laser.OnLaserChanged -= OnShowInfoLaser;
            _laser.OnReloadProgress -= OnShowRollbackLaser;
        }
        
        private void OnShowInfoLaser(int value)
        {
            _laserView.SetAmmonText($"Laser<br>Ammon: {value.ToString()}");
        }
        
        private void OnShowRollbackLaser(float time)
        {
            _laserView.SetReloadText($"Reload Time: {time.ToString("F1")}s");
        }
    }
}