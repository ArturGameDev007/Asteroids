using System;

namespace _Project.Scripts.Player.Weapons
{
    public interface ILaserState
    {
        public event Action<int> OnLaserChanged;
        public event Action<float> OnReloadProgress;

        public float ReloadTime { get; }
        public int CurrentAmmonLaser { get; }
    }
}