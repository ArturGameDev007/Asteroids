using Assets.Scripts.EnemySpace;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public abstract class TypesOfWeapon : MonoBehaviour
    {
        protected abstract void DestroyOfEnemies(Enemy enemy);
    }
}
