using Assets.Scripts.EnemySpace;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public abstract class TypesOfWeapon : MonoBehaviour
    {
        protected int PointForKill = 10;
        protected abstract void DestroyOfEnemies(Enemy enemy);
    }
}
