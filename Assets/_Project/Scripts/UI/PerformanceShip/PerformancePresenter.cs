using System;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformancePresenter: ITickable, IDisposable
    {
        private readonly ICoordinateView _coordinateView;
        private readonly ILaserView _laserView;
        private PlayerController _shipController;
        private GenerateLaser _laser;
        private Rigidbody2D _rigidbody2D;

        private bool _isReady;

        // public PerformancePresenter(ICoordinateView coordinateView, ILaserView laserView, PlayerController shipController, GenerateLaser laser)
        // {
        //     _coordinateView = coordinateView;
        //     _laserView = laserView;
        //     _shipController = shipController;
        //     _laser = laser;
        //     _rigidbody2D = shipController.GetComponent<Rigidbody2D>();
        // }
        //
        public PerformancePresenter(ICoordinateView coordinateView, ILaserView laserView)
        {
            _coordinateView = coordinateView;
            _laserView = laserView;
        }

        // public void Initialize()
        // {
        //     _laser.OnLaserChanged += OnShowInfoLaser;
        //     _laser.OnReloadProgress += OnShowRollbackLaser;
        //
        //     OnShowInfoLaser(_laser.CurrentAmmonLaser);
        //     OnShowRollbackLaser(_laser.LaserConfig.ReloadTime);
        // }

        public void Tick()
        {
            if (!_isReady)
                return;
            
            UpdateUICoordinate();
        }

        public void Setup(PlayerController playerController)
        {
            _shipController = playerController;
            
            _laser = playerController.GetComponent<GenerateLaser>();
            _rigidbody2D = playerController.GetComponent<Rigidbody2D>();

            _laser.OnLaserChanged += OnShowInfoLaser;
            _laser.OnReloadProgress += OnShowRollbackLaser;

            OnShowInfoLaser(_laser.CurrentAmmonLaser);
            OnShowRollbackLaser(_laser.LaserConfig.ReloadTime);
        
            _isReady = true;
        }

        public void Dispose()
        {
            _laser.OnLaserChanged -= OnShowInfoLaser;
            _laser.OnReloadProgress -= OnShowRollbackLaser;
        }

        private void UpdateUICoordinate()
        {
            Vector3 direction = _shipController.transform.position;

            float rotationAngleZ = _shipController.transform.localEulerAngles.z;
            float speed = _rigidbody2D.velocity.magnitude;

            string displayText = string.Format("<b>" + "X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}" + "</b>",
                direction.x, direction.y, rotationAngleZ, speed);

            _coordinateView.SetCoordinateText(displayText);
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