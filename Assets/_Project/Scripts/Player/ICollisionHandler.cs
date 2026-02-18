using System;
using _Project.Scripts.Enemies;

namespace _Project.Scripts.Player
{
    public interface ICollisionHandler
    {
        public event Action<IEnemy> OnCollisionDetected;
    }
}