using UnityEngine;

namespace _Project.Scripts.Player
{
    public interface IMovableEntity
    {
        public Vector3 Position { get; }
        public float RotationAngleZ { get; }
        public float Speed { get; }
    }
}