using System;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformancePresenter : ITickable, IDisposable
    {
        private readonly CoordinateResourceManager _coordinateResourceManager;
        private readonly IPlayerProvider _playerProvider;

        private bool _isInitialized;

        public PerformancePresenter(CoordinateResourceManager coordinateResourceManager, IPlayerProvider playerProvider)
        {
            _coordinateResourceManager = coordinateResourceManager;
            _playerProvider = playerProvider;
        }

        public void Tick()
        {
            if (_coordinateResourceManager.View == null)
                return;

            if (_playerProvider.Player == null || _playerProvider.LaserState == null)
                return;

            if (!_isInitialized)
                InitializeUICoordinate();

            UpdateUICoordinate();
        }

        private void InitializeUICoordinate()
        {
            var laserState = _playerProvider.LaserState;

            if (laserState == null)
                return;

            laserState.OnLaserChanged += OnShowInfoLaser;
            laserState.OnReloadProgress += OnShowRollbackLaser;

            OnShowInfoLaser(laserState.CurrentAmmonLaser);
            OnShowRollbackLaser(laserState.ReloadTime);

            _isInitialized = true;
        }

        public void Dispose()
        {
            var laserState = _playerProvider.LaserState;

            if (_isInitialized && laserState != null)
            {
                laserState.OnLaserChanged -= OnShowInfoLaser;
                laserState.OnReloadProgress -= OnShowRollbackLaser;
            }
        }

        private void UpdateUICoordinate()
        {
            var movable = _playerProvider.Player;

            Vector3 direction = movable.Position;

            float rotationAngleZ = movable.RotationAngleZ;
            float speed = movable.Speed;

            string displayText = string.Format("<b>" + "X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}" + "</b>",
                direction.x, direction.y, rotationAngleZ, speed);

            _coordinateResourceManager.View.SetCoordinateText(displayText);
        }

        private void OnShowInfoLaser(int value)
        {
            if (_coordinateResourceManager.View is ILaserView view)
                view.SetAmmonText($"Laser<br>Ammon: {value.ToString()}");
        }

        private void OnShowRollbackLaser(float time)
        {
            if (_coordinateResourceManager.View is ILaserView view)
                view.SetReloadText($"Reload Time: {time.ToString("F1")}s");
        }
    }
}