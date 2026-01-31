using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class WeaponShooter
    {
        public void CreateShoot(GameObject weapon, Transform spawnPoint)
        {
            Object.Instantiate(weapon, spawnPoint.position, spawnPoint.rotation);
        }
    }
}