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
        private readonly IMovableEntity _movableEntity;
        private readonly ILaserState _laserState;
        
        public PerformancePresenter(ICoordinateView coordinateView, ILaserView laserView,
            IMovableEntity movableEntity, ILaserState laserState)
        {
            _coordinateView = coordinateView;
            _laserView = laserView;
            _movableEntity = movableEntity;
            _laserState = laserState;
        }

        public void Initialize()
        {
            _laserState.OnLaserChanged += OnShowInfoLaser;
            _laserState.OnReloadProgress += OnShowRollbackLaser;
        
            OnShowInfoLaser(_laserState.CurrentAmmonLaser);
            OnShowRollbackLaser(_laserState.ReloadTime);
        }

        public void Tick()
        {
            UpdateUICoordinate();
        }

        public void Dispose()
        {
            _laserState.OnLaserChanged -= OnShowInfoLaser;
            _laserState.OnReloadProgress -= OnShowRollbackLaser;
        }

        private void UpdateUICoordinate()
        {
            Vector3 direction = _movableEntity.Position;

            float rotationAngleZ = _movableEntity.RotationAngleZ;
            float speed = _movableEntity.Speed;

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