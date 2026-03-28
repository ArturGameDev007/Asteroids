using System;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class PerformancePresenter : ITickable, IDisposable
    {
        // private readonly CoordinateResourceManager _uiManager;
        // private readonly IPlayerProvider _playerProvider;
        //
        // private bool _isInitialized;
        //
        // public PerformancePresenter(CoordinateResourceManager uiManager, IPlayerProvider playerProvider)
        // {
        //     _uiManager = uiManager;
        //     _playerProvider = playerProvider;
        // }
        //
        // public void Tick()
        // {
        //     if (_uiManager.View == null || _playerProvider.Player == null)
        //         return;
        //
        //     if (!_isInitialized)
        //         Initialize();
        //
        //     UpdateUI();
        // }
        //
        // private void Initialize()
        // {
        //     var player = _playerProvider.Player;
        //     var laser = player.GetComponent<ILaserState>();
        //
        //     if (laser != null)
        //     {
        //         laser.OnLaserChanged += OnShowInfoLaser;
        //         laser.OnReloadProgress += OnShowRollbackLaser;
        //
        //         OnShowInfoLaser(laser.CurrentAmmonLaser);
        //         OnShowRollbackLaser(laser.ReloadTime);
        //     }
        //
        //     _isInitialized = true;
        // }
        //
        // private void UpdateUI()
        // {
        //     var player = _playerProvider.Player;
        //     var view = _uiManager.View;
        //
        //     Vector3 pos = player.Position;
        //     string displayText = string.Format("<b>X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}</b>",
        //         pos.x, pos.y, player.RotationAngleZ, player.Speed);
        //
        //     view.SetCoordinateText(displayText);
        // }
        //
        // private void OnShowInfoLaser(int value)
        // {
        //     if (_uiManager.View is ILaserView laserView)
        //     {
        //         laserView.SetAmmonText($"Laser<br>Ammon: {value}");
        //     }
        // }
        //
        // private void OnShowRollbackLaser(float time)
        // {
        //     if (_uiManager.View is ILaserView laserView)
        //     {
        //         laserView.SetReloadText($"Reload Time: {time:F1}s");
        //     }
        // }
        //
        // public void Dispose()
        // {
        //     if (_isInitialized && _playerProvider.Player != null)
        //     {
        //         var laser = _playerProvider.Player.GetComponent<ILaserState>();
        //         if (laser != null)
        //         {
        //             laser.OnLaserChanged -= OnShowInfoLaser;
        //             laser.OnReloadProgress -= OnShowRollbackLaser;
        //         }
        //     }
        // }
        // private readonly ICoordinateView _coordinateView;

        private readonly CoordinateResourceManager _coordinateResourceManager;
        private readonly IMovableEntity _movableEntity;
        private readonly ILaserState _laserState;

        private bool _isInitialized;

        // public PerformancePresenter(ICoordinateView coordinateView, ILaserView laserView)
        // {
        //     _coordinateView = coordinateView;
        //     _laserView = laserView;
        // }

        public PerformancePresenter(CoordinateResourceManager coordinateResourceManager,  IMovableEntity movableEntity, ILaserState laserState)
        {
            _coordinateResourceManager = coordinateResourceManager;
            _movableEntity = movableEntity;
            _laserState = laserState;
        }

        // public void Setup(IMovableEntity movableEntity, ILaserState laserState)
        // {
        //     _movableEntity = movableEntity;
        //     _laserState = laserState;
        //
        //     _laserState.OnLaserChanged += OnShowInfoLaser;
        //     _laserState.OnReloadProgress += OnShowRollbackLaser;
        //
        //     OnShowInfoLaser(_laserState.CurrentAmmonLaser);
        //     OnShowRollbackLaser(_laserState.ReloadTime);
        //
        //     _isReady = true;
        // }

        // public void Initialize()
        // {
        //     _laserState.OnLaserChanged += OnShowInfoLaser;
        //     _laserState.OnReloadProgress += OnShowRollbackLaser;
        //
        //     OnShowInfoLaser(_laserState.CurrentAmmonLaser);
        //     OnShowRollbackLaser(_laserState.ReloadTime);
        // }

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