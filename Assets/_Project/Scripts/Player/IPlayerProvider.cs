using System;
using _Project.Scripts.Player.Weapons;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public interface IPlayerProvider
    {
        public event Action OnPlayerReady;
        
        public IMovableEntity MovableEntity { get; }
        public Transform PlayerTransform { get; }
        public PlayerController Player { get; }
        public ILaserState LaserState { get; }
        public ICollisionHandler CollisionHandler { get; }
        public IInputPauseHandler InputPauseHandler { get; }
        
        public void Setup(PlayerController playerController);
    }
}