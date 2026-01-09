using Scripts.Enemies;
using UnityEngine;

namespace Scripts.Weapons
{
    public abstract class TypesOfWeapon : MonoBehaviour
    {
        protected abstract void DestroyOfEnemies(Enemy enemy);
    }
}
