using Scripts.EnemySpace;
using UnityEngine;

namespace Scripts.Weapons
{
    public abstract class TypesOfWeapon : MonoBehaviour
    {
        protected abstract void DestroyOfEnemies(Enemy enemy);
    }
}
