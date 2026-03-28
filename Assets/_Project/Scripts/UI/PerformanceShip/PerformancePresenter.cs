using System;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformancePresenter : ITickable, IDisposable
    {
        private readonly CoordinateResourceManager _coordinateResourceManager;
        private readonly IMovableEntity _movableEntity;
        private readonly ILaserState _laserState;

        private bool _isInitialized;

        public PerformancePresenter(CoordinateResourceManager coordinateResourceManager,  IMovableEntity movableEntity, ILaserState laserState)
        {
            _coordinateResourceManager = coordinateResourceManager;
            _movableEntity = movableEntity;
            _laserState = laserState;
        }

        public void Tick()
        {
            if (_coordinateResourceManager.View == null)
                return;

            if (_movableEntity == null || _laserState == null)
                return;

            if (!_isInitialized)
                InitializeUICoordinate();

            UpdateUICoordinate();
        }
        
        private void InitializeUICoordinate()
        {
            _laserState.OnLaserChanged += OnShowInfoLaser;
            _laserState.OnReloadProgress += OnShowRollbackLaser;

            OnShowInfoLaser(_laserState.CurrentAmmonLaser);
            OnShowRollbackLaser(_laserState.ReloadTime);

            _isInitialized = true;
        }

        public void Dispose()
        {
            if (_isInitialized && _laserState != null)
            {
                _laserState.OnLaserChanged -= OnShowInfoLaser;
                _laserState.OnReloadProgress -= OnShowRollbackLaser;
            }
        }

        private void UpdateUICoordinate()
        {
            Vector3 direction = _movableEntity.Position;

            float rotationAngleZ = _movableEntity.RotationAngleZ;
            float speed = _movableEntity.Speed;

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