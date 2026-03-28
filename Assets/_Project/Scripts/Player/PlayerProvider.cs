using UnityEngine;

namespace _Project.Scripts.Player
{
    public class PlayerProvider : IPlayerProvider
    {
        public Transform PlayerTransform { get; private set; }
        
        public PlayerController Player { get; private set; }
        
        public void Setup(PlayerController playerController)
        {
            Player = playerController;
            PlayerTransform = Player.transform;
        }
    }
}