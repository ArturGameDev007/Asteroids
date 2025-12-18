using Assets.Scripts.EnemySpace;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;

namespace Assets.Scripts.Player.Weapons
{
    public abstract class TypesOfWeapon : MonoBehaviour
    {
        [SerializeField] protected ScoreManager Manager;

        protected int PointForKill = 10;

        protected abstract void DestroyOfEnemies(Enemy enemy);
    }
}
