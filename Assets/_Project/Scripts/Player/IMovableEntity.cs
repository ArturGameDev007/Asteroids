using UnityEngine;

namespace _Project.Scripts.Player
{
    public interface IMovableEntity
    {
        public Transform PlayerTransform { get; }
        
        public Vector3 Position { get; }
        public float RotationAngleZ { get; }
        public float Speed { get; }

        public void SetPaused(bool paused);
        public void ResetState();
        public void StopPhysics();
    }
}