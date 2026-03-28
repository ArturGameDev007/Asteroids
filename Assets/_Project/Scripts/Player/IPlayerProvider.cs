using UnityEngine;

namespace _Project.Scripts.Player
{
    public interface IPlayerProvider
    {
        public Transform PlayerTransform { get; }
        public PlayerController Player { get; }
        
        public void Setup(PlayerController playerController);
    }
}