using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public abstract class TypesOfWeapon : MonoBehaviour
    {
        protected abstract void DestroyOfEnemies(Enemy enemy);
    }
}
