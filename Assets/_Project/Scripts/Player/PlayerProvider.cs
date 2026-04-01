using System;
using _Project.Scripts.Player.Weapons;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerProvider : IPlayerProvider
    {
        public event Action OnPlayerReady;

        public IMovableEntity MovableEntity { get; private set; }
        public Transform PlayerTransform { get; private set; }
        public PlayerController Player { get; private set; }
        public ILaserState LaserState { get; private set; }
        public ICollisionHandler CollisionHandler { get; private set; }
        public IInputPauseHandler InputPauseHandler { get; private set; }

        public void Setup(PlayerController playerController)
        {
            Player = playerController;
            MovableEntity = playerController;
            PlayerTransform = playerController.transform;

            LaserState = playerController.GetComponent<ILaserState>();
            CollisionHandler = playerController.GetComponent<ICollisionHandler>();
            InputPauseHandler = playerController.GetComponent<IInputPauseHandler>();
            
            OnPlayerReady?.Invoke();
        }
    }
}